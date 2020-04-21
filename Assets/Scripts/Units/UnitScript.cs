using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class UnitScript : MonoBehaviour
{
    [FormerlySerializedAs("unitSelectionCircle")]
    [Header("Components")]
    [SerializeField] protected GameObject unitSelectionObject;

    public void Start() {
        unitSelectionObject.SetActive(false);
        UnitController.Instance.AddUnitInList(this);
        InitItems();
    }

    public void SelectionCircleIsActive(bool isActive) {
        unitSelectionObject.SetActive(isActive);
    }

    public virtual void Action(Vector3 target, GameObject targetObject) {
        
    }

    public virtual void InitItems() {
        
    }
}
