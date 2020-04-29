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

    [Header("Item Store")] 
    public Image unitIcon;
    public string nameItemShop;
    [TextArea]
    public string itemDescription;

    public int woodCost;
    public int rockCost;

}
