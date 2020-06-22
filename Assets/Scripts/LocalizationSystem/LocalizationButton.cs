using System;
using System.Collections;
using System.Collections.Generic;
using LocalizationSystem;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalizationButton : MonoBehaviour
{
    [SerializeField] private LanguageKey languageKey;

    private void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(SetGameLanguage);
    }

    private void SetGameLanguage()
    {
        LocalizationManager.instance.SetLanguageKey(languageKey);
        Debug.Log($"Language seted to: {languageKey}");
        
        Invoke("GoToScene", 1.0f);
    }

    private void GoToScene()
    {
        SceneManager.LoadScene("ProgrammingScene");
    }
}
