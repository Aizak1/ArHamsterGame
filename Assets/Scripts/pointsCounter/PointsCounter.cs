using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pointsCounter {
    public class PointsCounter : MonoBehaviour {
        public int points = 0;

        public void AddPoint() {
            points++;
        }

        public void RemovePoint() {
            points--;
        }
    }
}

