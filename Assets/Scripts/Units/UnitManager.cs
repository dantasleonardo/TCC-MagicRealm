using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class UnitManager : MonoBehaviour
{
    #region Singleton
    
    public static UnitManager Instance;
    
    private void Awake() {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(this);
    }
    
    #endregion


    [Header("Unit List")] public List<AttackUnitScriptable> allAttackUnits;
    public List<AttackUnitScriptable> ActiveAttackUnitsList;
    
    [Header("Cannon Robot")] 
    public string cannonRobotName;
    public int cannonRobotLevel;

    [Header("Cannon Robot")] 
    public string soldierRobotName;
    public int soldierRobotLevel;

    private void Start() {
        allAttackUnits = new List<AttackUnitScriptable>();
        ActiveAttackUnitsList = new List<AttackUnitScriptable>();
        
        var loadUnits = Resources.FindObjectsOfTypeAll<AttackUnitScriptable>();
        print($"Size of loadUnits: {loadUnits.Length}");
        
        foreach (var unit in loadUnits) {
            allAttackUnits.Add(unit);
        }

        ActiveAttackUnitsList.Add(GetAttackUnitScriptable(soldierRobotName + soldierRobotLevel.ToString()));
        ActiveAttackUnitsList.Add(GetAttackUnitScriptable(cannonRobotName + cannonRobotLevel.ToString()));
    }
    
    private AttackUnitScriptable GetAttackUnitScriptable(string unitDesired) {
        AttackUnitScriptable unit = ScriptableObject.CreateInstance<AttackUnitScriptable>();
        foreach (var attackUnit in allAttackUnits) {
            if (attackUnit.nameUnit == unitDesired)
                return attackUnit;
        }
        return unit;
    }
    

    // private List<AttackUnitScriptable> GetAttackUnitScriptable(string unitDesired) {
    //     var comparison = StringComparison.InvariantCulture;
    //
    //     return allAttackUnits.Where(unit => unit.nameUnit.StartsWith(unitDesired, comparison)).ToList();
    // }
    
    
}
