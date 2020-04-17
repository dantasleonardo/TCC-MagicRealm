using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUnit : Robot
{
    [Header("AttackUnit properties")]
    [SerializeField] private float firerateAttack;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Bullet bulletProperties;
    [SerializeField] private Transform spawnBulletPosition;

    private void Awake() {
        InitItem();
    }

    private void InitItem() {
        bulletProperties = Resources.Load<Bullet>($"CannonBallLevel1");
        if (bulletProperties != null) {
            bulletPrefab = bulletProperties.bulletPrefab;
        }
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            var bullet = Instantiate(bulletPrefab, spawnBulletPosition.position, transform.rotation);
            bullet.transform.forward = spawnBulletPosition.forward;
        }
    }
}
