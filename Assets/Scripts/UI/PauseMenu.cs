using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused;
    public GameObject panelPause;
    public Slider slider;

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

    private void Start() {
        slider.value = GameManager.Instance.volume; 
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Paused();
        }
        GameManager.Instance.volume = slider.value;
        GameController.Instance.audioSource.volume = GameManager.Instance.volume;
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
