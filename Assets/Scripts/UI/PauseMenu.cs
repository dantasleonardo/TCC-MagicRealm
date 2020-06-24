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
        var volumeUser = SaveSystem.SaveSystem.Load().volume;
        slider.onValueChanged.AddListener(value =>
        {
            var user = SaveSystem.SaveSystem.Load();
            user.volume = value;
            SaveSystem.SaveSystem.Save(user);
        });
        GameManager.instance.volume = volumeUser;
        slider.value = volumeUser;
        AudioListener.volume = volumeUser;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Paused();
        }
        GameManager.instance.volume = slider.value;
        AudioListener.volume = GameManager.instance.volume;
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
