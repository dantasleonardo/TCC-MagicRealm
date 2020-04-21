using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRobotAnimation : MonoBehaviour
{
    [Header("Idle")] [SerializeField] private Transform target;
    [SerializeField] private float frequency;
    [SerializeField] private float magnitude;


    private AttackUnit unit;

    private void Start() {
        unit = GetComponent<AttackUnit>();
    }

    void Update() {
        target.transform.position += Vector3.up * (Mathf.Sin(Time.time * frequency) * (magnitude / 1000f));
    }
}