using MissionScripts;
using UnityEngine;
using UnityEngine.UI;

public class MissionToDestroy : MonoBehaviour, IMission
{
    [SerializeField] private Mission properties;
    [SerializeField] private Text missionText;
    [SerializeField] private int count;

    public void Init(Mission mission)
    {
        properties = mission;
        missionText = GetComponent<Text>();
        missionText.text = properties.title;
        count = 0;
    }

    public bool MissionCompleted()
    {
        switch (properties.destroyType)
        {
            case DestroyType.Crystals:
                count = GameController.Instance.crystalsDestroyed;
                if (count >= properties.amountToDestroyed)
                {
                    missionText.text =
                        $"{properties.textUI} {properties.amountToDestroyed}/{properties.amountToDestroyed}";
                    missionText.color = Color.gray;
                }
                else
                {
                    missionText.text = $"{properties.textUI} {count}/{properties.amountToDestroyed}";
                    missionText.color = Color.white;
                }

                return count >= properties.amountToDestroyed;
            case DestroyType.Base:
                if (GameController.Instance.magicCastle.life < 1)
                {
                    missionText.text = $"{properties.textUI}";
                    missionText.color = Color.gray;
                }
                else
                {
                    missionText.text = $"{properties.textUI}";
                    missionText.color = Color.white;
                }

                return GameController.Instance.magicCastle.life < 1;
        }

        return false;
    }
}