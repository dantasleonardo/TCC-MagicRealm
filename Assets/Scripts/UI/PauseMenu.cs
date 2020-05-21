using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused;
    public GameObject panelPause;

    public void Paused()
    {
        if(gameIsPaused == false)
        {
            gameIsPaused = true;
            Time.timeScale = 0f;
            panelPause.SetActive(true);
        }
        else
        {
            gameIsPaused = false;
            Time.timeScale = 1f;
            panelPause.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Paused();
        }
    }

    public void Resume()
    {
        Paused();
    }

    public void MenuPrincipal()
    {
        Paused();
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Debug.Log("Fechou");
        Application.Quit();
    }

}
