using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Robot : UnitScript, IUnit {
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

    public void TakeDamage(int damage) {
        life -= damage;
        Debug.Log($"Take damage: {damage} and life: {life}");
        if(life <= 0) {
            UnitController.Instance.units.Remove(this);
            Destroy(this.gameObject);
        }
    }
}


public enum RobotType
{
    Attack,
    Gatherer
}