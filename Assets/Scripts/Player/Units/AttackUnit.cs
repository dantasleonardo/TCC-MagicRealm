using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEditorInternal;
using UnityEngine;

public class AttackUnit : Robot
{
    [Header("AttackUnit properties")] [SerializeField]
    private string fileNameOfUnit;

    public AttackUnitScriptable attackUnitProperties;

    [SerializeField] private float attakDistance;
    [SerializeField] private float firerateAttack;
    [SerializeField] private float turningSpeed;

    [SerializeField] private string fileNameOfBullet;
    [SerializeField] private Bullet bulletProperties;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnBulletPosition;


    private GameObject currentTarget;
    private bool attacking = false; 
    private float nextTimeToFire;

    private void Awake() {
        InitItem();
    }

    private void InitItem() {
        bulletProperties = Resources.Load<Bullet>(fileNameOfBullet);
        attackUnitProperties = Resources.Load<AttackUnitScriptable>(fileNameOfUnit);

        if (bulletProperties != null) {
            bulletPrefab = bulletProperties.bulletPrefab;
        }

        if (attackUnitProperties != null) {
            life = attackUnitProperties.life;
            speedMovement = attackUnitProperties.speedMovement;
            attakDistance = attackUnitProperties.attackDistace;
            firerateAttack = attackUnitProperties.firerateAttack;
            turningSpeed = attackUnitProperties.turningSpeed;
        }
    }

    public override void Action(Vector3 target, GameObject targetObject) {
        if (targetObject.CompareTag("Mages")) {
            agent.stoppingDistance = attakDistance;
            currentTarget = targetObject;
            MoveTo(target);
        }
        else if(targetObject.CompareTag("Ground")){
            agent.stoppingDistance = 0.0f;
            attacking = false;
            currentTarget = null;
            MoveTo(target);
        }
    }

    private void Update() {
        if (attacking) {
            // transform.LookAt(currentTarget.transform);
            var lookRotation = LookTarget();
            
            if (Time.time > nextTimeToFire && Vector3.Magnitude(lookRotation.eulerAngles - transform.rotation.eulerAngles) < 0.5f) {
                var bullet = Instantiate(bulletPrefab, spawnBulletPosition.position, transform.rotation);
                bullet.transform.forward = spawnBulletPosition.forward;
                nextTimeToFire = Time.time + (1f / firerateAttack);
            }
        }

        if (Vector3.Distance(transform.position, agent.destination) <= attakDistance && currentTarget != null) {
            attacking = true;
        }
        else if (currentTarget != null && currentTarget.CompareTag("Mages")) {
            attacking = false;
            MoveTo(currentTarget.transform.position);
        }
    }

    private Quaternion LookTarget() {
        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);
        
        return lookRotation;
    }
}