using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LocalizationSystem
{
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

            Invoke(nameof(GoToScene), 0.1f);
        }

        private void GoToScene()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}