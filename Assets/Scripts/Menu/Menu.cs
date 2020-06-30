using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider slider;


    private void Start()
    {
        if (SaveSystem.SaveSystem.Load() != null)
        {
            var volumeUser = SaveSystem.SaveSystem.Load().volume;
            GameManager.instance.volume = volumeUser;
            if (slider)
                slider.value = volumeUser;
            AudioListener.volume = volumeUser;
        }

        if (slider)
        {
            slider.value = GameManager.instance.volume;
            slider.onValueChanged.AddListener(value =>
            {
                var user = SaveSystem.SaveSystem.Load();
                user.volume = value;
                SaveSystem.SaveSystem.Save(user);
            });
        }
    }

    private void Update()
    {
        if (slider)
            GameManager.instance.volume = slider.value;
        AudioListener.volume = GameManager.instance.volume;
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