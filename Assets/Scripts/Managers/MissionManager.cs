using System;
using System.Collections.Generic;
using MissionScripts;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private List<Mission> missions;
    [SerializeField] private GameObject missionPrefab;
    [SerializeField] private Transform missionParent;
    [SerializeField] private List<bool> missionsCompleted;

    private void Start()
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
            go.name = m.title;
        });
    }

    private void Update()
    {
;
    }

    private void GetTypeMission()
    {
        
    }

    private List<bool> GetMissionsCompleted()
    {
        return new List<bool>();
    }

    private void GameOver()
    {
        
    }

    private void WonTheGame()
    {
        
    }
}
