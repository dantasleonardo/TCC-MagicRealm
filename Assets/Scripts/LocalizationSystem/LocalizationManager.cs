using System;
using UnityEngine;

namespace LocalizationSystem
{
    public class LocalizationManager : MonoBehaviour
    {
        #region Singleton

        public static LocalizationManager instance;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this.gameObject);
            
            DontDestroyOnLoad(this.gameObject);
        }

        #endregion

        [SerializeField] private LanguageKey languageKey;

        public LanguageKey GetLanguageKey()
        {
            return languageKey;
        }

        public void SetLanguageKey(LanguageKey key)
        {
            languageKey = key;
            var texts = GameObject.FindGameObjectsWithTag("Text");
            foreach (var text in texts)
            {
                var normalText = text.GetComponent<LocalizationText>();
                var textMeshPro = text.GetComponent<LocalizationTextMeshPro>();
                if(textMeshPro != null)
                    textMeshPro.UpdateText();
                else if(normalText != null)
                    normalText.UpdateText();
            }
        }
    }
}
