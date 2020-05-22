using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    public string url;

    public void linkFeedback() {
        Application.OpenURL(url);
    }
}
