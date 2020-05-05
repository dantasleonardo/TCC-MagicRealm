using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStoreItem : MonoBehaviour
{
    private ItemStore itemStore;
    private Button buttonItem;


    private void Start() {
        itemStore = GetComponent<ItemStore>();
        buttonItem = GetComponent<Button>();
        itemStore.Init();
        buttonItem.onClick.AddListener(itemStore.BuyItem);
    }
}
