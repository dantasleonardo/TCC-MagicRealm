using System.Collections.Generic;
using System.Linq;
using MissionScripts;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private List<Mission> missions;
    [SerializeField] private GameObject missionPrefab;
    [SerializeField] private Transform missionParent;
    [SerializeField] private List<GameObject> missionsInScene;

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
        Debug.Log("Lose The Game ");
        GetComponent<MissionsDirector>().PlayWinLoseCutscene($"You Lose!");
    }

    private void WonTheGame()
    {
        Debug.Log("Won The Game ");
        GetComponent<MissionsDirector>().PlayWinLoseCutscene($"You Win!");
    }
}