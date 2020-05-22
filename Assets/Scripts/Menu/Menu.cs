using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;

    private void Start() {
        slider.value = GameManager.Instance.volume;
    }

    private void Update() {
        GameManager.Instance.volume = slider.value;
        audioSource.volume = GameManager.Instance.volume;
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
