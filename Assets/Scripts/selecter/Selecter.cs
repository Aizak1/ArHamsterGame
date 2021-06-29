using hamster;
using UnityEngine;

namespace selecter {
    public class Selecter : MonoBehaviour {
        private void Update() {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (!Physics.Raycast(ray, out RaycastHit hit)) {
                    return;
                }

                var hamster = hit.transform.GetComponent<Hamster>();
                if (hamster == null) {
                    return;
                }

                hamster.isTouched = true;
                Destroy(hamster.gameObject);
            }
        }
    }
}

