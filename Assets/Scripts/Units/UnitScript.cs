﻿using UnityEngine;
using UnityEngine.Serialization;

public class UnitScript : MonoBehaviour
{
    [FormerlySerializedAs("unitSelectionCircle")] [Header("Components")] [SerializeField]
    protected GameObject unitSelectionObject;

    public void Start()
    {
        unitSelectionObject.SetActive(false);
        UnitController.Instance.AddUnitInList(this);
        InitItems();
    }


    public virtual void SelectionObjectIsActive(bool isActive)
    {
        unitSelectionObject.SetActive(isActive);
    }

    public virtual void Action(Vector3 target, GameObject targetObject = null)
    {
    }

    public virtual void InitItems()
    {
    }
}