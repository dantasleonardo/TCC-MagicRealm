using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    [SerializeField] private List<AudioSource> audioScenes;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }

    private void Update()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        switch (currentScene)
        {
            case "Menu":
                if (!audioScenes[0].isPlaying)
                    PlayMusic(0);
                break;
            case "Credits":
                if (!audioScenes[0].isPlaying)
                    PlayMusic(0);
                break;
            case "TutorialLevel1":
                if (!audioScenes[1].isPlaying)
                    PlayMusic(1);
                break;
            case "TutorialLevel2":
                if (!audioScenes[1].isPlaying)
                    PlayMusic(1);
                break;
            case "Nivel1":
                if (!audioScenes[2].isPlaying)
                    PlayMusic(2);
                break;
            case "Nivel2":
                if (!audioScenes[3].isPlaying)
                    PlayMusic(3);
                break;
            case "Nivel3":
                if (!audioScenes[4].isPlaying)
                    PlayMusic(4);
                break;
        }
    }

    private void PlayMusic(int index)
    {
        for (int i = 0; i < audioScenes.Count; i++)
        {
            if(i == index)
                audioScenes[i].Play();
            else
                audioScenes[i].Stop();
        }
    }
}