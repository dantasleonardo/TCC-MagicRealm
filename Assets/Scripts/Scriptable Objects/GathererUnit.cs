using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Gatherer Character", menuName = "Units/Gatherer Unit/Character")]
public class GathererUnit : ScriptableObject
{
    [Header("Properties")] public string nameUnit;
    public int life;
    public float speedMovement;
    public int maxAmountResources;
    public float gatheringSpeed;
    public int amountResources;
    public GameObject unitPrefab;
    public float turningSpeed;
    public RobotType RobotType = RobotType.Gatherer;
    
    [Header("Item Store")] 
    public Sprite unitIcon;
    public string nameItemShopEn;
    public string nameItemShopPt;
    [TextArea]
    public string itemDescriptionEn;
    [TextArea]
    public string itemDescriptionPt;

    public int woodCost;
    public int stoneCost;

    [Header("Spawn Item")] 
    public float currentTimeToCreate = 0.0f;

    public float timeToCreate = 2.0f;
}