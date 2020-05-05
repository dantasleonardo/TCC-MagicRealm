using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Singleton

    public static GameController Instance;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion

    public int startAmountWood;
    public int startAmountRock;
    public Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    [Header("UI")] 
    public Text woodText;

    public Text rockText;
    
    [FormerlySerializedAs("instantiateUnits")] [Header("Create Units")]
    public List<ItemCreation> stackCreation = new List<ItemCreation>();

    private void Start() {
        resources.Add(ResourceType.Wood, startAmountWood);
        resources.Add(ResourceType.Rock, startAmountRock);
        
        UpdateResourcesUi();
    }

    private void Update() {
        if (stackCreation.Count > 0) {
            ItemCreation creation = stackCreation[0];
            creation.currentTimeToCreate += Time.deltaTime;

            if (creation.currentTimeToCreate > creation.timeToCreate) {
                InstantiateUnit(creation.prefab);
                stackCreation.RemoveAt(0);
                creation.currentTimeToCreate = 0.0f;
            }
        }
    }
    #region InstantiateUnits

    public void InstantiateUnit(GameObject unitPrefab) {
        var spawn = MainBase.Instance.spawnPosition.position;
        var unit = Instantiate(unitPrefab, spawn, Quaternion.identity);

        
        var attackUnit = unit.GetComponent<AttackUnitScript>();
        attackUnit.InitItems();
        
        attackUnit.MoveTo(MainBase.Instance.destinationPosition.position);
    }

    #endregion

    private void UpdateResourcesUi() {
        woodText.text = $"Wood: {resources[ResourceType.Wood].ToString()}";
        rockText.text = $"Rock: {resources[ResourceType.Rock].ToString()}";

    }

    public void GetResources(Dictionary<ResourceType, int> _resources) {
        foreach (var resource in _resources) {
            resources[resource.Key] += resource.Value;
        }
        UpdateResourcesUi();
    }
}
