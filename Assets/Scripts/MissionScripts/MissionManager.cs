using System.Collections.Generic;
using System.Linq;
using LocalizationSystem;
using MissionScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private List<Mission> missions;
    [SerializeField] private GameObject missionPrefab;
    [SerializeField] private Transform missionParent;
    [SerializeField] private List<GameObject> missionsInScene;
    [SerializeField] private Button tryAgainOrContinue;
    [SerializeField] private string nextScene;
    [SerializeField] private GameObject canvasToDisable;

    private void Start()
    {
        GetTypeMission();
    }

    private void Update()
    {
        var missionsCompleted = GetMissionsCompleted();
        if (GameController.Instance.robotsCastle.life < 1)
            GameOver();
        if (GameController.Instance.magicCastle.life < 1)
            WonTheGame();
        if (missionsCompleted == missions.Count)
            WonTheGame();
    }

    private void GetTypeMission()
    {
        missions.ForEach(m =>
        {
            var go = Instantiate(missionPrefab, missionParent);
            switch (m.missionType)
            {
                case MissionType.Collect:
                    go.AddComponent<CollectingMission>();
                    break;
                case MissionType.Destroy:
                    go.AddComponent<MissionToDestroy>();
                    break;
            }

            go.GetComponent<IMission>().Init(m);
            missionsInScene.Add(go);
            go.name = m.title;
        });
    }

    private int GetMissionsCompleted()
    {
        var list = missionsInScene.Where(m => m.GetComponent<IMission>().MissionCompleted()).ToList();
        return list.Count;
    }

    private void GameOver()
    {
        switch (LocalizationManager.instance.GetLanguageKey())
        {
            case LanguageKey.English:
                GetComponent<MissionsDirector>().PlayWinLoseCutscene($"You Lose!");
                break;
            case LanguageKey.Portuguese:
                GetComponent<MissionsDirector>().PlayWinLoseCutscene($"Você Perdeu!");
                break;
        }
        if (tryAgainOrContinue)
        {
            var textButton = tryAgainOrContinue.GetComponentInChildren<Text>();
            if (textButton)
            {
                switch (LocalizationManager.instance.GetLanguageKey())
                {
                    case LanguageKey.English:
                        textButton.text = "Try Again";
                        break;
                    case LanguageKey.Portuguese:
                        textButton.text = "Tentar Novamente";
                        break;
                }
            }
            {
                switch (LocalizationManager.instance.GetLanguageKey())
                {
                    case LanguageKey.English:
                        textButton.text = "Try Again";
                        break;
                    case LanguageKey.Portuguese:
                        textButton.text = "Tentar Novamente";
                        break;
                }
            }
            tryAgainOrContinue.onClick.AddListener(() =>
            {
                var transition = LoadingScene.Instance;
                if (transition)
                {
                    transition.scene = SceneManager.GetActiveScene().name;
                    transition.StartTransition();
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                canvasToDisable.SetActive(false);
            });
        }
    }

    private void WonTheGame()
    {
        switch (LocalizationManager.instance.GetLanguageKey())
        {
            case LanguageKey.English:
                GetComponent<MissionsDirector>().PlayWinLoseCutscene($"You Win!");
                break;
            case LanguageKey.Portuguese:
                GetComponent<MissionsDirector>().PlayWinLoseCutscene($"Você Ganhou!");
                break;
        }
        var textButton = tryAgainOrContinue.GetComponentInChildren<Text>();
        if (textButton)
        {
            switch (LocalizationManager.instance.GetLanguageKey())
            {
                case LanguageKey.English:
                    textButton.text = "Continue";
                    break;
                case LanguageKey.Portuguese:
                    textButton.text = "Próximo Nível";
                    break;
            }
        }
        tryAgainOrContinue.onClick.AddListener(() =>
        {
            var transition = LoadingScene.Instance;
            if (transition)
            {
                transition.scene = nextScene;
                transition.StartTransition();
            }
            else
            {
                SceneManager.LoadScene(nextScene);
            }

            canvasToDisable.SetActive(false);
        });
        var user = SaveSystem.SaveSystem.Load();
        var scene = SceneManager.GetActiveScene().name;
        switch (scene)
        {
            case "Nivel1":
                user.levelsCompleted[1] = true;
                SaveSystem.SaveSystem.Save(user);
                break;
            case "Nivel2":
                user.levelsCompleted[2] = true;
                SaveSystem.SaveSystem.Save(user);
                break;
        }
    }
}