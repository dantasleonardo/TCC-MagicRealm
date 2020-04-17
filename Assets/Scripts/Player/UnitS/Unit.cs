using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [Header("Unit Properties")] [SerializeField]
    protected int life;
    
    
    [Header("Components")]
    [SerializeField] protected GameObject unitSelectionCircle;

    public void Start() {
        unitSelectionCircle.SetActive(false);
        UnitController.Instance.AddUnitInList(this);
        InitItems();
    }

    public void SelectionCircleIsActive(bool isActive) {
        unitSelectionCircle.SetActive(isActive);
    }

    public virtual void Action(Vector3 target, GameObject targetObject) {
        
    }

    public virtual void InitItems() {
        
    }
}
