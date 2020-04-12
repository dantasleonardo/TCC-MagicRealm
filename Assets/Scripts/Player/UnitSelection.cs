
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Rendering;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private LayerMask unitLayerMask;

    [SerializeField] private List<Unit> selectedUnits = new List<Unit>();
    [SerializeField] private Vector2 startPosition;

    [Header("Components")] 
    [SerializeField] private Camera mainCamera;
    [SerializeField] private UnitController unitController;

    private void Awake() {
        mainCamera = Camera.main;
        unitController = GetComponent<UnitController>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            selectedUnits = new List<Unit>();
            startPosition = Input.mousePosition;
            
            TrySelect(Input.mousePosition);
        }

        if (Input.GetMouseButton(0)) {
            UpdateSelectionBox(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) {
            ReleaseSelectionBox();
        }
    }

    private void TrySelect(Vector2 screenPosition) {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, unitLayerMask)) {
            Unit unit = hit.collider.GetComponent<Unit>();
            Debug.Log("Object Selected");
            selectedUnits.Add(unit);
        }
    }
    
    private void UpdateSelectionBox(Vector2 currentMousePosition) {
        if(!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        float width = currentMousePosition.x - startPosition.x;
        float height = currentMousePosition.y - startPosition.y;
        
        selectionBox.sizeDelta = new Vector2(Math.Abs(width), Math.Abs(height));
        selectionBox.anchoredPosition = startPosition + new Vector2(width / 2, height / 2);
    }

    private void ReleaseSelectionBox() {
        selectionBox.gameObject.SetActive(false);

        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);
        
        unitController.units.ForEach(unit => {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(unit.transform.position);

            if (screenPosition.x >= min.x && screenPosition.y >= min.y && screenPosition.x <= max.x &&
                screenPosition.y <= max.y) {
                selectedUnits.Add(unit);
                Debug.Log("Object Selected");
            }
        });
    }
}
