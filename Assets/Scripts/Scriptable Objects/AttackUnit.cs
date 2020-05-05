using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Attack Character", menuName = "Units/Attack Unit/Character")]
public class AttackUnit : ScriptableObject
{
    [Header("Properties")] 
    public string nameUnit;
    public int life;
    public float speedMovement;
    public float attackDistace;
    public float firerateAttack;
    public float turningSpeed;
    public GameObject unitPrefab;
    public string fileNameOfBullet;
    public RobotType RobotType = RobotType.Attack;

    [Header("Item Store")] 
    public Sprite unitIcon;
    public string nameItemShop;
    [TextArea]
    public string itemDescription;

    public int woodCost;
    public int rockCost;

    [Header("Spawn Item")] 
    public float currentTimeToCreate = 0.0f;

    public float timeToCreate = 2.0f;
}
