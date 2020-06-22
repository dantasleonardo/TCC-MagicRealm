using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour
{
    private void Start()
    {
        var user = SaveSystem.SaveSystem.Load();
        if (user != null)
            SceneManager.LoadScene("Menu");
        else
            SceneManager.LoadScene("SetLanguageScene");
    }
}
