using planesController;
using pointsCounter;
using UnityEngine;
using UnityEngine.UI;

namespace ui {
    public class UiController : MonoBehaviour {
        [SerializeField]
        private PointsCounter pointsCounter;
        [SerializeField]
        private ARPlanesController planesController;

        [SerializeField]
        private Text pointsText;
        [SerializeField]
        private Text noPlanesText;

        private void Update() {
            pointsText.text = pointsCounter.points.ToString();

            if (planesController.planes.Count != 0) {
                noPlanesText.enabled = false;
            } else {
                noPlanesText.enabled = true;
            }
        }
    }
}

