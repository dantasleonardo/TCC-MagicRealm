using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Play()
    {

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
