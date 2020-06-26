using UnityEngine;
using UnityEngine.SceneManagement;

public class Feedback : MonoBehaviour
{
    public string url;

    public void LinkFeedback()
    {
        Application.OpenURL(url);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel(string Level)
    {
        LoadingScene.Instance.scene = Level;
        LoadingScene.Instance.StartTransition();
    }
}