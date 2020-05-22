using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Audio;

    public void Start()
    {
        Audio = GameObject.Find("Audio");
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Play()
    {
        SceneManager.LoadScene("Alpha");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }


    public void Quit()
    {
        Debug.Log("Fechou");
        Application.Quit();
    }
 
}
