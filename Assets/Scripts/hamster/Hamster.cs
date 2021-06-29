using pointsCounter;
using UnityEngine;

namespace hamster {
    public class Hamster : MonoBehaviour {

        public bool isTouched;
        private const int LIFE_TIME = 10;

        private float destroyTime;

        public PointsCounter pointsCounter;

        private void Start() {
            destroyTime = Time.time + LIFE_TIME;
            isTouched = false;
        }

        private void Update() {

            transform.LookAt(Camera.main.transform);

            if (Time.time >= destroyTime) {
                Destroy(gameObject);
            }
        }

        private void OnDestroy() {
            if (isTouched) {
                pointsCounter.AddPoint();
            } else {
                pointsCounter.RemovePoint();
            }
        }
    }
}

