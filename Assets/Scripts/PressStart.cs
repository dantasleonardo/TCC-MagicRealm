using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour
{
    private void Start()
    {
        var user = SaveSystem.SaveSystem.Load();
        if (user != null)
        {
            Debug.Log(user.ToString());
            SceneManager.LoadScene("Menu");
        }
        else
            SceneManager.LoadScene("SetLanguageScene");
    }
}
