using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : Unit
{
    [Header("Robots Properties")]
    [SerializeField] protected float speedMovement = 2.0f;
    
    protected NavMeshAgent agent;

    #region FunctionsOfUnit

    public override void Action(Vector3 target, GameObject targetObject) {
        if (targetObject.CompareTag("Ground")) {
            MoveTo(target);
        }
    }

    public override void InitItems() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speedMovement;
    }

    #endregion
    
    
    private void MoveTo(Vector3 target) {
        agent.SetDestination(target);
    }
}
