using TMPro;
using UnityEngine;

namespace LocalizationSystem
{
    public class LocalizationTextMeshPro : MonoBehaviour
    {
        [SerializeField] private string textInEnglish;
        [SerializeField] private string textInPortuguese;
        [SerializeField] private TextMeshProUGUI textInHud;

        private void Start()
        {
            if (textInHud == null)
                textInHud = GetComponent<TextMeshProUGUI>();
            UpdateText();
        }

        private void OnEnable()
        {
            if (textInHud == null)
                textInHud = GetComponent<TextMeshProUGUI>();
            UpdateText();
        }

        public void UpdateText()
        {
            switch (LocalizationManager.instance.GetLanguageKey())
            {
                case LanguageKey.English:
                    textInHud.text = textInEnglish;
                    break;
                case LanguageKey.Portuguese:
                    textInHud.text = textInPortuguese;
                    break;
            }
        }
    }
}