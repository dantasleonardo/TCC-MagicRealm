using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToLevel : MonoBehaviour
{
    [SerializeField] private string desiredScene;
    [SerializeField] private GameObject transition;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            transition.SetActive(true);
            LoadingScene.Instance.scene = desiredScene;
            LoadingScene.Instance.StartTransition();
            MainMenuManager.instance.gameObject.SetActive(false);
        });
    }
}
