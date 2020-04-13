using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject unitSelectionCircle;

    private void Start() {
        unitSelectionCircle.SetActive(false);
    }

    public void SelectionCircleIsActive(bool isActive) {
        unitSelectionCircle.SetActive(isActive);
    }
}
