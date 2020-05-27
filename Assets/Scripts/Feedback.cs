using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Feedback : MonoBehaviour
{
    public string url;

    public void linkFeedback() {
        Application.OpenURL(url);
    }

    public void BackToMenu() {
        SceneManager.LoadScene("Menu");
    }
}
