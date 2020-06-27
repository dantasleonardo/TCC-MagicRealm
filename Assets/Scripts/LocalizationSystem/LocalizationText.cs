using UnityEngine;
using UnityEngine.UI;

namespace LocalizationSystem
{
    public class LocalizationText : MonoBehaviour
    {
        [SerializeField] private string textInEnglish;
        [SerializeField] private string textInPortuguese;
        [SerializeField] private Text textInHud;

        private void Start()
        {
            if (textInHud == null)
            {
                textInHud = GetComponent<Text>();
                UpdateText();
            }
        }

        private void OnEnable()
        {
            if (textInHud == null)
            {
                textInHud = GetComponent<Text>();
                UpdateText();
            }
            else
            {
                UpdateText();
            }
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