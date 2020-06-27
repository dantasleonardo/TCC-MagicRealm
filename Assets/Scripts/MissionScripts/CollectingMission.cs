using LocalizationSystem;
using UnityEngine;
using UnityEngine.UI;

namespace MissionScripts
{
    public class CollectingMission : MonoBehaviour, IMission
    {
        [SerializeField] private Mission properties;
        [SerializeField] private Text missionText;
        [SerializeField] private int count;

        public void Init(Mission mission)
        {
            properties = mission;
            missionText = GetComponent<Text>();
            missionText.text = SetLanguageText(properties.resourceType);
        }

        [SerializeField]
        public bool MissionCompleted()
        {
            switch (properties.resourceType)
            {
                case ResourceType.Stone:
                    count = GameController.Instance.stonesCollected;
                    break;
                case ResourceType.Wood:
                    count = GameController.Instance.woodCollected;
                    break;
            }

            if (count >= properties.amountOfResources)
            {
                missionText.text = SetLanguageText(properties.resourceType);
                missionText.color = Color.gray;
            }
            else
            {
                missionText.text = SetLanguageText(properties.resourceType);
                missionText.color = Color.white;
            }

            return count >= properties.amountOfResources;
        }

        private string SetLanguageText(ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Stone:
                    switch (LocalizationManager.instance.GetLanguageKey())
                    {
                        case LanguageKey.English:
                            var textEnglish = count >= properties.amountOfResources
                                ? $"{properties.textUI_En} {properties.amountOfResources}/{properties.amountOfResources}"
                                : $"{properties.textUI_En} {GameController.Instance.stonesCollected}/{properties.amountOfResources}";
                            return textEnglish;
                        case LanguageKey.Portuguese:
                            var textPortuguese = count >= properties.amountOfResources
                                ? $"{properties.textUI_Pt} {properties.amountOfResources}/{properties.amountOfResources}"
                                : $"{properties.textUI_Pt} {GameController.Instance.stonesCollected}/{properties.amountOfResources}";
                            return textPortuguese;
                    }

                    break;
                case ResourceType.Wood:
                    switch (LocalizationManager.instance.GetLanguageKey())
                    {
                        case LanguageKey.English:
                            var textEnglish = count >= properties.amountOfResources
                                ? $"{properties.textUI_En} {properties.amountOfResources}/{properties.amountOfResources}"
                                : $"{properties.textUI_En} {GameController.Instance.woodCollected}/{properties.amountOfResources}";
                            return textEnglish;
                        case LanguageKey.Portuguese:
                            var textPortuguese = count >= properties.amountOfResources
                                ? $"{properties.textUI_Pt} {properties.amountOfResources}/{properties.amountOfResources}"
                                : $"{properties.textUI_Pt} {GameController.Instance.woodCollected}/{properties.amountOfResources}";
                            return textPortuguese;
                    }

                    break;
            }

            return " ";
        }
    }
}