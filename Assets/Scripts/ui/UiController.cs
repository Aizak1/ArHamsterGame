using pointsCounter;
using UnityEngine;
using UnityEngine.UI;

namespace ui {
    public class UiController : MonoBehaviour {
        [SerializeField]
        private PointsCounter pointsCounter;

        [SerializeField]
        private Text pointsText;
        [SerializeField]
        private Text noPlanesText;

        private void Update() {
            pointsText.text = pointsCounter.points.ToString();
            noPlanesText.enabled = false;
        }
    }
}

