using hamster;
using pointsCounter;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace spawner {
    public class Spawner : MonoBehaviour {
        [SerializeField]
        private PointsCounter pointsCounter;

        [SerializeField]
        private ARRaycastManager raycastManager;

        [SerializeField]
        private Hamster hamsterToSpawn;

        private float spawnTime = 0;

        private List<ARRaycastHit> hits = new List<ARRaycastHit>();

        private const int MIN_SPAWN_TIME = 2;
        private const int MAX_SPAWN_TIME = 8;

        private const float MAX_DISTANCE = 5f;

        private const float HEIGHT_OFFSET = 0.08f;

        private void Update() {

            var width = Screen.width;
            var height = Screen.height;

            var randomWidth = Random.Range(width / 4, width * 3 / 4);
            var centerHeight = (height / 2) - height * HEIGHT_OFFSET;

            var spawnPoint = new Vector2(randomWidth, centerHeight);

            hits.Clear();
            raycastManager.Raycast(spawnPoint, hits, TrackableType.PlaneWithinBounds);

            if(hits.Count > 0) {
                var pos = hits[0].pose.position;
                var rot = hits[0].pose.rotation;

                transform.position = pos;
                transform.rotation = rot;

            } else {
                return;
            }


            if (Time.time >= spawnTime && gameObject.activeInHierarchy) {

                var position = transform.position;
                var distance = Vector3.Distance(Camera.main.transform.position, position);

                if(distance > MAX_DISTANCE) {
                    return;
                }

                var hamster = Instantiate(hamsterToSpawn, position, Quaternion.identity);
                hamster.transform.LookAt(Camera.main.transform);
                hamster.pointsCounter = pointsCounter;

                int randomSpawnTime = Random.Range(MIN_SPAWN_TIME, MAX_SPAWN_TIME);
                spawnTime = Time.time + randomSpawnTime;
            }
        }

    }
}

