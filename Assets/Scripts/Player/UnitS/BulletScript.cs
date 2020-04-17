using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Transactions;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletScript : MonoBehaviour
{
    public Bullet bulletProperties;
    [SerializeField] private int damage;
    [SerializeField] private float speed;

    [SerializeField] private Vector3 bulletDirection;
    
    private Rigidbody rigidbody;

    private void Awake() {
        InitItem();
        Debug.Log(Vector3.forward);
    }

    private void Start() {
        Destroy(this.gameObject, 5.0f);
    }

    private void InitItem() {
        damage = bulletProperties.damage;
        speed = bulletProperties.speed;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        rigidbody.velocity = transform.forward * speed;
    }
}
