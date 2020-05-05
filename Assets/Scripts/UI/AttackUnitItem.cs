using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUnitItem : ItemStore
{
    public AttackUnit unitProperties;
    private ItemCreation itemCreation = new ItemCreation();

    public override void Init() {
        itemImage.sprite = unitProperties.unitIcon;
        itemCreation.timeToCreate = unitProperties.timeToCreate;
        itemCreation.prefab = unitProperties.unitPrefab;
    }
    
    public override void BuyItem() {
        GameController.Instance.stackCreation.Add(itemCreation);
    }
}
