using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererUnitItem : ItemStore {
    public GathererUnit unitProperties;
    private ItemCreation itemCreation = new ItemCreation();
    public ButtonStoreItem buttonStoreItem;


    public override void Init() {
        itemImage.sprite = unitProperties.unitIcon;
        itemCreation.timeToCreate = unitProperties.timeToCreate;
        itemCreation.prefab = unitProperties.unitPrefab;
        itemCreation.rockCost = unitProperties.rockCost;
        itemCreation.woodCost = unitProperties.woodCost;
        itemCreation.robotType = unitProperties.RobotType;
    }

    public override void BuyItem() {
        if (GameController.Instance.stackCreation.Count < 10) {
            GameController.Instance.resources[ResourceType.Stone] -= unitProperties.rockCost;
            GameController.Instance.resources[ResourceType.Wood] -= unitProperties.woodCost;

            GameController.Instance.stackCreation.Add(itemCreation);
            GameController.Instance.InstantiateItemLoading(unitProperties.unitIcon);
        }
    }

    private void Update() {
        var resources = GameController.Instance.resources;
        if (resources[ResourceType.Stone] >= unitProperties.rockCost &&
            resources[ResourceType.Wood] >= unitProperties.woodCost)
            buttonStoreItem.buttonItem.interactable = true;
        else
            buttonStoreItem.buttonItem.interactable = false;

    }
}
