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
            missionText.text = $"{properties.textUI} {count}/{properties.amountOfResources}";
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
                missionText.text = $"{properties.textUI} {properties.amountOfResources}/{properties.amountOfResources}";
                missionText.color = Color.gray;
            }
            else
            {
                missionText.text = $"{properties.textUI} {count}/{properties.amountOfResources}";
                missionText.color = Color.white;
            }

            return count >= properties.amountOfResources;
        }
    }
}