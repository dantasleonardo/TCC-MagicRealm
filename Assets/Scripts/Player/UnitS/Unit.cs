using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] protected GameObject unitSelectionCircle;

    protected NavMeshAgent agent;

    private void Start() {
        unitSelectionCircle.SetActive(false);
        UnitController.Instance.AddUnitInList(this);
        agent = GetComponent<NavMeshAgent>();
    }

    public void SelectionCircleIsActive(bool isActive) {
        unitSelectionCircle.SetActive(isActive);
    }

    public virtual void Action(Vector3 target, GameObject targetObject) {
        
    }
}
