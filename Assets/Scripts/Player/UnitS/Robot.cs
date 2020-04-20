using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : UnitScript
{
    [Header("Robots Properties")] [SerializeField]
    protected float speedMovement = 2.0f;

    protected NavMeshAgent agent;

    #region FunctionsOfUnit

    public override void InitItems() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speedMovement;
    }

    #endregion


    protected void MoveTo(Vector3 target) {
        agent.SetDestination(target);
    }
}