using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
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
    
    [Header("Create Units")]
    public List<ItemCreation> stackCreation = new List<ItemCreation>();
    public List<CreationLoading> stackLoadings = new List<CreationLoading>();
    public GameObject itemLoadingPrefab;
    public Transform itemLoadingParent;

    private void Start() {
        resources.Add(ResourceType.Wood, startAmountWood);
        resources.Add(ResourceType.Rock, startAmountRock);
        
        UpdateResourcesUi();
    }

    private void Update() {
        if (stackCreation.Count > 0) {
            ItemCreation creation = stackCreation[0];
            creation.currentTimeToCreate += Time.deltaTime;

            stackLoadings[0].loadingBar.fillAmount = creation.currentTimeToCreate / creation.timeToCreate;
            
            if (creation.currentTimeToCreate > creation.timeToCreate) {
                InstantiateUnit(creation.prefab, creation.robotType);
                RemoveItemOfCreations(0);
            }
        }
    }
    #region InstantiateUnits

    private void InstantiateUnit(GameObject unitPrefab, RobotType type) {
        var spawn = MainBase.Instance.spawnPosition.position;
        var spawnRotation = MainBase.Instance.spawnPosition.rotation;
        var unit = Instantiate(unitPrefab, spawn, spawnRotation);

        if (type == RobotType.Attack) {
            var attackUnit = unit.GetComponent<AttackUnitScript>();
            attackUnit.InitItems();
        
            attackUnit.MoveTo(MainBase.Instance.destinationPosition.position);
        }
        if (type == RobotType.Gatherer) {
            var gatherer = unit.GetComponent<GahtererUnitScript>();
            gatherer.InitItems();
        
            gatherer.MoveTo(MainBase.Instance.destinationPosition.position);
        }
    }

    public void InstantiateItemLoading(Sprite icon) {
        UpdateResourcesUi();
        var prefab = Instantiate(itemLoadingPrefab, itemLoadingParent);
         var loading = prefab.GetComponent<CreationLoading>();
         
         loading.icon.sprite = icon;
        
        stackLoadings.Add(loading);
        var indexOf = stackLoadings.IndexOf(loading);

        stackLoadings[indexOf].index = indexOf;
    }

    public void ClickToRemoveItemOfCreations(int index) {
        resources[ResourceType.Rock] += stackCreation[index].rockCost;
        resources[ResourceType.Wood] += stackCreation[index].woodCost;
        UpdateResourcesUi();

        if (index == 0) {
            stackCreation[index].currentTimeToCreate = 0.0f;
        }
        stackCreation.RemoveAt(index);
        Destroy(stackLoadings[index].gameObject);
        stackLoadings.RemoveAt(index);

        foreach (var stack in stackLoadings) {
            var indexOf = stackLoadings.IndexOf(stack);
            stack.index = indexOf;
        }
        
    }
    
    public void RemoveItemOfCreations(int index) {
        UpdateResourcesUi();

        if (index == 0) {
            stackCreation[index].currentTimeToCreate = 0.0f;
        }
        stackCreation.RemoveAt(index);
        Destroy(stackLoadings[index].gameObject);
        stackLoadings.RemoveAt(index);

        foreach (var stack in stackLoadings) {
            var indexOf = stackLoadings.IndexOf(stack);
            stack.index = indexOf;
        }
        
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
