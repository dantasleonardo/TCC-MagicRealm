using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public GameObject Menu;
    public bool pressAnyKey;
    public GameObject PressAnyKey;

    public void activeMenu()
    {
        if (Input.anyKeyDown)
        {
            Menu.SetActive(true);
            PressAnyKey.SetActive(false);
            //SceneManager.LoadScene("Menu");
        }
    }

    private void Update()
    {
        activeMenu();
    }
}
