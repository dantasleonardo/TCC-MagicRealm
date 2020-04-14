using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject unitSelectionCircle;

    private NavMeshAgent agent;

    private void Start() {
        unitSelectionCircle.SetActive(false);
        UnitController.Instance.AddUnitInList(this);
        agent = GetComponent<NavMeshAgent>();
    }

    public void SelectionCircleIsActive(bool isActive) {
        unitSelectionCircle.SetActive(isActive);
    }

    public void MoveTo(Vector3 target) {
        agent.SetDestination(target);
    }
}
