using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Slider slider;
    

    private void Start() {
        if (SaveSystem.SaveSystem.Load() != null)
        {
            var volumeUser = SaveSystem.SaveSystem.Load().volume;
            GameManager.instance.volume = volumeUser;
            slider.value = volumeUser;
            AudioListener.volume = volumeUser;

        }
        slider.value = GameManager.instance.volume;
        slider.onValueChanged.AddListener(value =>
        {
            var user = SaveSystem.SaveSystem.Load();
            user.volume = value;
            SaveSystem.SaveSystem.Save(user);
        });

    }

    private void Update() {
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
