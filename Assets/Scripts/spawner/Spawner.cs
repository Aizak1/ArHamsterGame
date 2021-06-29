using hamster;
using planesController;
using pointsCounter;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace spawner {
    public class Spawner : MonoBehaviour {
        [SerializeField]
        private ARPlanesController planesController;
        [SerializeField]
        private PointsCounter pointsCounter;

        [SerializeField]
        private Hamster hamsterToSpawn;

        private float spawnTime = 0;

        private const int MIN_SPAWN_TIME = 2;
        private const int MAX_SPAWN_TIME = 8;

        private const float MIN_DISTANCE = 0.15f;
        private const float MAX_DISTANCE = 5f;

        private void Update() {

            if (planesController.planes.Count == 0) {
                return;
            }

            if (Time.time >= spawnTime) {

                var position = GetHamsterPosition();

                var hamster = Instantiate(hamsterToSpawn, position, Quaternion.identity);
                hamster.transform.LookAt(Camera.main.transform);
                hamster.pointsCounter = pointsCounter;

                int randomSpawnTime = Random.Range(MIN_SPAWN_TIME, MAX_SPAWN_TIME);
                spawnTime = Time.time + randomSpawnTime;
            }
        }

        private Vector3 GetHamsterPosition() {

            var planesCopy = new List<ARPlane>(planesController.planes);
            Vector3 hamsterPosition;
            while (planesCopy.Count != 0) {
                int randomPlaneIndex = Random.Range(0, planesCopy.Count);
                var plane = planesCopy[randomPlaneIndex];

                if (plane.transform.position.y > Camera.main.transform.position.y) {
                    planesCopy.Remove(plane);
                    continue;
                }

                var posX = plane.center.x + Random.Range(-plane.size.x / 3, plane.size.x / 3);
                var posZ = plane.center.z + Random.Range(-plane.size.y / 3, plane.size.y / 3);

                hamsterPosition = new Vector3(posX, plane.center.y, posZ);

                var distance = Vector3.Distance(Camera.main.transform.position, hamsterPosition);

                if (distance < MIN_DISTANCE || distance > MAX_DISTANCE) {
                    planesCopy.Remove(plane);
                    continue;
                }
                return hamsterPosition;
            }

            var planeIndex = Random.Range(0, planesController.planes.Count);
            return planesController.planes[planeIndex].center;

        }
    }
}

