using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class CreateUnitWindow : EditorWindow
{
    #region AttackUnit

    private string nameUnit;
    private int life = 0;
    private float speedMovement = 0.0f;
    private float attackDistace = 0.0f;
    private float firerateAttack = 0.0f;
    private float turningSpeed = 0.0f;
    private GameObject unitPrefab;
    private string fileNameOfBullet;

    #endregion


    [MenuItem("Magic Realm/Create Unit")]
    public static void ShowWindow() {
        GetWindow<CreateUnitWindow>("Create Unit");
    }

    private void OnGUI() {
        GUILayout.Label("Create a Attack Unit", EditorStyles.boldLabel);
        nameUnit = EditorGUILayout.TextField("Unit name", nameUnit);
        life = EditorGUILayout.IntField("Life", life);
        attackDistace = EditorGUILayout.FloatField("Attack Distance", attackDistace);
        firerateAttack = EditorGUILayout.FloatField("Firerate", firerateAttack);
        turningSpeed = EditorGUILayout.FloatField("Turning Speed", turningSpeed);
        unitPrefab = (GameObject)EditorGUILayout.ObjectField(unitPrefab, typeof(GameObject), false);
        fileNameOfBullet = EditorGUILayout.TextField("File name of bullet", fileNameOfBullet);

        if (GUILayout.Button("Create Attack Unit")) {
            if (nameUnit != null) {
                var newAttackUnit = TryCreateAttackUnit();
                AssetDatabase.CreateAsset(newAttackUnit, $"Assets/Units/Resources/{nameUnit}.asset");
            }
        }
    }

    private AttackUnitScriptable TryCreateAttackUnit() {
        AttackUnitScriptable attackUnit = ScriptableObject.CreateInstance<AttackUnitScriptable>();
        attackUnit.life = life;
        attackUnit.speedMovement = speedMovement;
        attackUnit.attackDistace = attackDistace;
        attackUnit.firerateAttack = firerateAttack;
        attackUnit.turningSpeed = turningSpeed;
        attackUnit.unitPrefab = unitPrefab;
        attackUnit.fileNameOfBullet = fileNameOfBullet;
        
        return attackUnit;
    }
}