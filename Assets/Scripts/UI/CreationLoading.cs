using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationLoading : MonoBehaviour
{
    public Image icon;
    public Image loadingBar;
    public int index;
    public Button button;

    private void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(CancelCreationUnit);
    }

    private void CancelCreationUnit() {
        GameController.Instance.ClickToRemoveItemOfCreations(index);
    }
}
