using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GabrielCassimiro.Utilities
{
    public static class Utilities
    {

        public static Vector3 GetWorldPosition() {
            Vector3 vector = GetMouseWorldPosition(Input.mousePosition, Camera.main);
            vector.z = 0.0f;
            return vector;
        }

        public static Vector3 GetWorldPositionWithZ() {
            Vector3 vector = GetMouseWorldPosition(Input.mousePosition, Camera.main);
            return vector;
        }
        
        public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera camera) {
            Vector3 worldPosition = camera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
    }
}
