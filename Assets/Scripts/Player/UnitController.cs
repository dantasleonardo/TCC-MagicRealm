using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public static UnitController Instance;
    
    public List<Unit> units = new List<Unit>();

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void AddUnitInList(Unit unit) {
        units.Add(unit);
    }

    public void RemoveUnitOfList(Unit unit) {
        units.Remove(unit);
    }
}
