using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
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

    private void Start() {
        resources.Add(ResourceType.Wood, startAmountWood);
        resources.Add(ResourceType.Rock, startAmountRock);
        
        UpdateResourcesUi();
    }

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
