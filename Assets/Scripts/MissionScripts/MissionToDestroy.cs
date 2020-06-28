using LocalizationSystem;
using MissionScripts;
using UnityEngine;
using UnityEngine.UI;

public class MissionToDestroy : MonoBehaviour, IMission
{
    [SerializeField] private Mission properties;
    [SerializeField] private Text missionText;
    [SerializeField] private int count;


    [SerializeField] private bool cutsceneUsed;

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
                    missionText.text = SetLanguageText(DestroyType.Crystals);
                    missionText.color = Color.gray;
                    // var dome = GameObject.FindWithTag("Dome");
                    // if(dome != null)
                    //     dome.GetComponent<Dome>().DestroyDome();
                    if (!cutsceneUsed)
                    {
                        Invoke(nameof(PlayDomeCutscene), 2.6f);
                        cutsceneUsed = true;
                    }
                }
                else
                {
                    missionText.text = SetLanguageText(DestroyType.Crystals);
                    missionText.color = Color.white;
                }

                return count >= properties.amountToDestroyed;
            case DestroyType.Base:
                if (GameController.Instance.magicCastle.life < 1)
                {
                    missionText.text = SetLanguageText(DestroyType.Base);
                    missionText.color = Color.gray;
                }
                else
                {
                    missionText.text = SetLanguageText(DestroyType.Base);
                    missionText.color = Color.white;
                }

                return GameController.Instance.magicCastle.life < 1;
        }

        return false;
    }

    private void PlayDomeCutscene()
    {
        MissionsDirector.instance.PlayCutsceneOfDome();
    }

    private string SetLanguageText(DestroyType destroyType)
    {
        switch (destroyType)
        {
            case DestroyType.Crystals:
                switch (LocalizationManager.instance.GetLanguageKey())
                {
                    case LanguageKey.English:
                        var textEnglish = count >= properties.amountToDestroyed
                            ? $"{properties.textUI_En} {properties.amountToDestroyed}/{properties.amountToDestroyed}"
                            : $"{properties.textUI_En} {GameController.Instance.crystalsDestroyed}/{properties.amountToDestroyed}";
                        return textEnglish;
                    case LanguageKey.Portuguese:
                        var textPortuguese = count >= properties.amountToDestroyed
                            ? $"{properties.textUI_Pt} {properties.amountToDestroyed}/{properties.amountToDestroyed}"
                            : $"{properties.textUI_Pt} {GameController.Instance.crystalsDestroyed}/{properties.amountToDestroyed}";
                        return textPortuguese;
                }

                break;
            case DestroyType.Base:
                switch (LocalizationManager.instance.GetLanguageKey())
                {
                    case LanguageKey.English:
                        return missionText.text = $"{properties.textUI_En}";
                    case LanguageKey.Portuguese:
                        return missionText.text = $"{properties.textUI_Pt}";
                }

                break;
        }

        return " ";
    }
}