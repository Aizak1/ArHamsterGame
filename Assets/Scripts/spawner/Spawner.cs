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
        private GameObject marker;

        [SerializeField]
        private Hamster hamsterToSpawn;

        private float spawnTime = 0;

        private List<ARRaycastHit> hits = new List<ARRaycastHit>();

        private const int MIN_SPAWN_TIME = 2;
        private const int MAX_SPAWN_TIME = 8;

        private const float MIN_DISTANCE = 0.15f;
        private const float MAX_DISTANCE = 5f;

        private void Start() {
            marker.SetActive(false);
        }

        private void Update() {

            var width = Screen.width;
            var height = Screen.height;
            var center = new Vector2(width/ 2, (height / 2) - height * 0.08f);

            hits.Clear();
            raycastManager.Raycast(center, hits, TrackableType.PlaneWithinBounds);

            if(hits.Count > 0) {
                var targetPos = hits[0].pose.position;
                var targetRot = hits[0].pose.rotation;

                marker.transform.position = targetPos;
                marker.transform.rotation = targetRot;

                marker.SetActive(true);

            } else {
                marker.SetActive(false);
            }


            if (Time.time >= spawnTime && marker.activeInHierarchy) {

                var position = GetHamsterPosition();

                var hamster = Instantiate(hamsterToSpawn, position, Quaternion.identity);
                hamster.transform.LookAt(Camera.main.transform);
                hamster.pointsCounter = pointsCounter;

                int randomSpawnTime = Random.Range(MIN_SPAWN_TIME, MAX_SPAWN_TIME);
                spawnTime = Time.time + randomSpawnTime;
            }
        }

        private Vector3 GetHamsterPosition() {
            return marker.transform.position;
        }
    }
}

