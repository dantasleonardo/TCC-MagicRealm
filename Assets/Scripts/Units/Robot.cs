using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : UnitScript
{
    [Header("Robots Properties")] 
    [SerializeField] protected int life;
    [SerializeField] protected float speedMovement = 2.0f;

    protected NavMeshAgent agent;

    #region FunctionsOfUnit

    #endregion

    protected void MoveTo(Vector3 target) {
        agent.SetDestination(target);
    }
}