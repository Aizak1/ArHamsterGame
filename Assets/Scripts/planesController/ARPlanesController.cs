using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace planesController {
    public class ARPlanesController : MonoBehaviour {

        public ARPlaneManager arPlaneManager;
        public List<ARPlane> planes;

        private void OnEnable() {
            planes = new List<ARPlane>();
            arPlaneManager.planesChanged += OnPlanesChanged;
        }

        private void OnDisable() {
            arPlaneManager.planesChanged -= OnPlanesChanged;
        }

        private void OnPlanesChanged(ARPlanesChangedEventArgs args) {
            if (args.added != null && args.added.Count > 0) {
                planes.AddRange(args.added);
            }

            if (args.removed != null && args.removed.Count > 0) {
                foreach (var item in args.removed) {
                    planes.Remove(item);
                }
            }
        }

    }
}

