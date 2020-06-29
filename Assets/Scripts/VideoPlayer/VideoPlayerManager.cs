using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private List<VideoClip> videoClipsPt;
    [SerializeField] private List<VideoClip> videoClipsEn;
    [SerializeField] private LanguageKey languageKey;
    [SerializeField] private Button nextVideoButton;
    [SerializeField] private Button skipVideoButton;
    [SerializeField] private string sceneName;
    private int count;

    private void Start()
    {
        switch (languageKey)
        {
            case LanguageKey.Portuguese:
                videoPlayer.clip = videoClipsPt[0];
                videoPlayer.Play();
                break;
            case LanguageKey.English:
                videoPlayer.clip = videoClipsEn[0];
                videoPlayer.Play();
                break;
        }
        
        nextVideoButton.onClick.AddListener(NextVideo);
        skipVideoButton.onClick.AddListener(SkipVideos);
    }

    private void NextVideo()
    {
        switch (languageKey)
        {
            case LanguageKey.Portuguese:
                if (++count < videoClipsPt.Count)
                {
                    videoPlayer.Stop();
                    videoPlayer.clip = videoClipsPt[count];
                    videoPlayer.Play();
                }
                else
                {
                    SceneManager.LoadScene(sceneName);
                }
                break;
            case LanguageKey.English:
                if (++count < videoClipsEn.Count)
                {
                    videoPlayer.Stop();
                    videoPlayer.clip = videoClipsEn[count];
                    videoPlayer.Play();
                }
                else
                {
                    var transition = LoadingScene.Instance;
                    if(!transition)
                        SceneManager.LoadScene(sceneName);
                    else
                    {
                        transition.scene = sceneName;
                        transition.waitTime = 2.5f;
                        transition.StartTransition();
                    }
                }
                break;
        }
    }

    private void SkipVideos()
    {
        SceneManager.LoadScene(sceneName);
    }
}