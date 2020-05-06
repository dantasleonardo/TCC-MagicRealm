using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class LoadMenu : MonoBehaviour
{
    public GameObject Menu;
    public bool keyIsPressed;
    public GameObject PressAnyKey;
    public Animator menuAnimation;

    public void activeMenu()
    {
        Menu.SetActive(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown && !keyIsPressed) 
        {
            PressAnyKey.SetActive(false);
            keyIsPressed = true;
            menuAnimation.SetBool("KeyPressed", keyIsPressed);
        }
    }
}
