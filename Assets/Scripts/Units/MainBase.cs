using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainBase : Building
{
    #region Singleton

    public static MainBase Instance;


    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion
    
    
    [SerializeField] private GameObject functionsPanel;
    public Transform spawnPosition;
    public Transform destinationPosition;


    private bool isSelected = false;

    [Header("Animations")] 
    [SerializeField] private float animationTimeFunctionsPanel;


    public override void InitItems() {
        FunctionsPanelIsActive();
        
    }

    public override void SelectionObjectIsActive(bool isActive) {
        isSelected = isActive;
        base.SelectionObjectIsActive(isActive);
        if(isActive)
            OpenFunctionsPanel();
        else
            CloseFunctionsPanel();
    }

    #region UnitRelated
    
    public void GetResourcesOfUnit(Dictionary<ResourceType, int> resources){
        GameController.Instance.GetResources(resources);
    }
    
    #endregion

    #region UIAnimation

    private void OpenFunctionsPanel() {
        FunctionsPanelIsActive();
        LeanTween.moveY(functionsPanel, -10.0f, animationTimeFunctionsPanel);
    }

    private void CloseFunctionsPanel() {
        LeanTween.moveY(functionsPanel, -250.0f, animationTimeFunctionsPanel).setOnComplete(FunctionsPanelIsActive);
    }

    private void FunctionsPanelIsActive() {
        functionsPanel.SetActive(isSelected);
    }

    #endregion
}
