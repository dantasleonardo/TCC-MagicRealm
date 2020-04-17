﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private LayerMask unitLayerMask;

    [SerializeField] private List<Unit> selectedUnits = new List<Unit>();
    private List<Unit> forDesactiveUnits = new List<Unit>();
    [SerializeField] private Vector2 startPosition;

    [Header("Components")] [SerializeField]
    private Camera mainCamera;

    [SerializeField] private UnitController unitController;

    private void Awake() {
        mainCamera = Camera.main;
        unitController = GetComponent<UnitController>();
    }

    private void Update() {
        //Mouse actions with left button.
        if (Input.GetMouseButtonDown(0)) {
            if (selectedUnits.Count > 0) {
                if (Input.GetKey(KeyCode.LeftControl)) {
                    
                }
                else {
                    selectedUnits = new List<Unit>();
                    DesactiveSelectionUnit();
                }
            }
            else {
                if (Input.GetKey(KeyCode.LeftControl)) {
                    
                }
                else {
                    selectedUnits = new List<Unit>();
                }
            }

            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0)) {
            UpdateSelectionBox(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) {
            if (startPosition.Equals(Input.mousePosition))
                TrySelect(Input.mousePosition);
            else
                ReleaseSelectionBox();
        }

        //Mouse actions with right button
        if (Input.GetMouseButtonDown(1)) {
            SetDestinationOfUnits();
        }
    }

    #region Selection Unit

    private void UpdateSelectionBox(Vector2 currentMousePosition) {
        if (!selectionBox.gameObject.activeInHierarchy)
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
                unit.SelectionCircleIsActive(true);
                selectedUnits.Add(unit);
                forDesactiveUnits.Add(unit);
            }
        });
    }

    private void TrySelect(Vector2 screenPosition) {
        var ray = mainCamera.ScreenPointToRay(screenPosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, unitLayerMask)) {
            Unit unit = hit.collider.GetComponent<Unit>();
            
            selectedUnits.Add(unit);
            forDesactiveUnits.Add(unit);
            unit.SelectionCircleIsActive(true);
        }
    }

    private void DesactiveSelectionUnit() {
        foreach (var unit in forDesactiveUnits) {
            unit.SelectionCircleIsActive(false);
        }

        forDesactiveUnits = new List<Unit>();
    }

    #endregion

    #region MoveUnits

    private void SetDestinationOfUnits() {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.CompareTag("Ground")) {
                var target = hit.point;
                
                int AxisZ = 0;
                List<Vector3> listOfTargets = new List<Vector3>() {
                    target,
                    target - new Vector3(-1,0,0),
                    target - new Vector3(1,0,0)
                };

                int count = 0;
                
                foreach (var unit in selectedUnits) {
                    var desiredTarget = listOfTargets[count];
                    desiredTarget.z -= AxisZ;
                    unit.Action(desiredTarget, hit.collider.gameObject);
                    count = (count + 1) % listOfTargets.Count;
                    if (count == 0)
                        AxisZ--;
                }
            }
        }
    }

    #endregion
}