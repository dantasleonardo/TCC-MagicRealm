using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateBulletsUnitsWindow : EditorWindow
{
    #region BulletProperties

    private string bulletName;
    private int damage;
    private float speed;
    private GameObject bulletPrefab;

    #endregion


    [MenuItem("Magic Realm/Create Bullet Unit")]
    public static void ShowWindow() {
        GetWindow<CreateBulletsUnitsWindow>("Create Bullet Unit");
    }

    private void OnGUI() {
        GUILayout.Label("Create bullet of unit", EditorStyles.boldLabel);
        
        bulletName = EditorGUILayout.TextField("Name of bullet", bulletName);
        damage = EditorGUILayout.IntField("Damage of bullet", damage);
        speed = EditorGUILayout.FloatField("Speed of bullet", speed);
        bulletPrefab = (GameObject) EditorGUILayout.ObjectField(bulletPrefab, typeof(GameObject), false);

        if (GUILayout.Button("Create Bullet")) {
            if (bulletName != null) {
                var bullet = CreateBullet();
                AssetDatabase.CreateAsset(bullet, $"Assets/Units/Resources/{bulletName}.asset");
            }
        }
    }

    private Bullet CreateBullet() {
        Bullet newBullet = ScriptableObject.CreateInstance<Bullet>();
        newBullet.damage = damage;
        newBullet.speed = speed;
        newBullet.bulletPrefab = bulletPrefab;

        return newBullet;
    }
}