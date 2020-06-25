using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class MissionsDirector : MonoBehaviour
{
    public static MissionsDirector instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] PlayableDirector director;
    [SerializeField] PlayableDirector cutsceneDome;
    [SerializeField] Text screenText;

    public void PlayWinLoseCutscene(string text)
    {
        director.gameObject.SetActive(true);
        screenText.text = text;
        director.Play();
    }

    public void PlayCutsceneOfDome()
    {
        cutsceneDome.Play();
    }
}