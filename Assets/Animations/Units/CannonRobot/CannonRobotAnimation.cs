using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRobotAnimation : MonoBehaviour
{
    [Header("Idle")] [SerializeField] private Transform target;
    [SerializeField] private float frequency;
    [SerializeField] private float magnitude;


    private AttackUnitScript unitScript;

    private void Start() {
        unitScript = GetComponent<AttackUnitScript>();
    }

    void Update() {
        target.transform.position += Vector3.up * (Mathf.Sin(Time.time * frequency) * (magnitude / 1000f));
    }
}