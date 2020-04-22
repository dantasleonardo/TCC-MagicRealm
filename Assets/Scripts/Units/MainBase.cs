using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainBase : Building
{
    [SerializeField] private GameObject functionsPanel;
    [SerializeField] private Transform spawnPosition;
    
    
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
    
    
    
    #endregion

    #region UIAnimation

    private void OpenFunctionsPanel() {
        FunctionsPanelIsActive();
        LeanTween.moveY(functionsPanel, 0.0f, animationTimeFunctionsPanel);
    }

    private void CloseFunctionsPanel() {
        LeanTween.moveY(functionsPanel, -300.0f, animationTimeFunctionsPanel).setOnComplete(FunctionsPanelIsActive);
    }

    private void FunctionsPanelIsActive() {
        functionsPanel.SetActive(isSelected);
    }

    #endregion
}
