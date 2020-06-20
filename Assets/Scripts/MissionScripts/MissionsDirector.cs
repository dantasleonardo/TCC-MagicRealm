using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class MissionsDirector : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    [SerializeField] Text screenText;

    public void PlayWinLoseCutscene(string text)
    {
        director.gameObject.SetActive(true);
        screenText.text = text;
        director.Play();
    }
}