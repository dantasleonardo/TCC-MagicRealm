using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GabrielCassimiro.Utilities;
public class UnitControl : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;

    [SerializeField] private Vector3 startPosition;
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            startPosition = GabrielCassimiro.Utilities.Utilities.GetWorldPosition();
            Debug.Log($"start position: {startPosition}");
        }

        if (Input.GetMouseButtonUp(0)) {
            var endPosition = GabrielCassimiro.Utilities.Utilities.GetWorldPosition();
            
            Vector3 lowerLeftPosition = new Vector3(Math.Min(startPosition.x, endPosition.x), Math.Min(startPosition.y, endPosition.y), 0);
            Vector3 upperRightPosition = new Vector3(Math.Max(startPosition.x,endPosition.x), Math.Max(startPosition.y,endPosition.y), 0);

            objects.ForEach(currentObject => {
                var currentPosition = currentObject.transform.position;
                if (currentPosition.x >= lowerLeftPosition.x && currentPosition.y >= lowerLeftPosition.y &&
                    currentPosition.x <= upperRightPosition.x && currentPosition.y <= upperRightPosition.y) {
                    Debug.Log("Deu certo");
                }
            });
            
            Debug.Log($"end position: {endPosition}");
        }
    }
}
