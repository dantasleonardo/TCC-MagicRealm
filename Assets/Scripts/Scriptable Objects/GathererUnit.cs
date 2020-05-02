using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}