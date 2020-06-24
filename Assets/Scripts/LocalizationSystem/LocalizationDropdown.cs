using System;
using UnityEngine;
using TMPro;

namespace LocalizationSystem
{
    public class LocalizationDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;

        private void Start()
        {
            dropdown = GetComponent<TMP_Dropdown>();
            switch (LocalizationManager.instance.GetLanguageKey())
            {
                case LanguageKey.Portuguese:
                    dropdown.value = 0;
                    break;
                case LanguageKey.English:
                    dropdown.value = 1;
                    break;
            }
            dropdown.onValueChanged.AddListener(ChangeLanguage);
        }

        private void ChangeLanguage(int value)
        {
            var user = SaveSystem.SaveSystem.Load();
            switch (value)
            {
                case 0:
                    LocalizationManager.instance.SetLanguageKey(LanguageKey.Portuguese);
                    user.languageKey = LanguageKey.Portuguese;
                    SaveSystem.SaveSystem.Save(user);
                    break;
                case 1:
                    LocalizationManager.instance.SetLanguageKey(LanguageKey.English);
                    user.languageKey = LanguageKey.English;
                    SaveSystem.SaveSystem.Save(user);
                    break;
            }
        }
    }
}
