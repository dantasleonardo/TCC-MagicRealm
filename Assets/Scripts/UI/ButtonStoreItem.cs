using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStoreItem : MonoBehaviour
{
    private ItemStore itemStore;


    private void Start() {
        itemStore = GetComponent<ItemStore>();
    }
}
