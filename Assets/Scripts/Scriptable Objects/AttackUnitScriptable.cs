using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Character", menuName = "Units/Attack Unit/Character")]
public class AttackUnitScriptable : ScriptableObject
{
    [Header("Properties")]
    public int life;
    public float speedMovement;
    public float attackDistace;
    public float firerateAttack;
    public float turningSpeed;
    public GameObject unitPrefab;
    public string fileNameOfBullet;
}
