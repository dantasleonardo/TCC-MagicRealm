using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Robot : UnitScript
{
    [Header("Robots Properties")] 
    [SerializeField] protected int life;
    [SerializeField] protected float speedMovement = 2.0f;
    public RobotType robotType;

    protected NavMeshAgent agent;

    #region FunctionsOfUnit

    #endregion

    public void MoveTo(Vector3 target) {
        agent.SetDestination(target);
    }
}


public enum RobotType
{
    Attack,
    Gatherer
}