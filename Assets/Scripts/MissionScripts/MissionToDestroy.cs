using System.Collections;
using System.Collections.Generic;
using MissionScripts;
using UnityEngine;
using UnityEngine.UI;

public class MissionToDestroy : MonoBehaviour, IMission
{
    [SerializeField] private Mission properties;
    [SerializeField] private Text missionText;

    public void Init(Mission mission)
    {
        properties = mission;
        missionText = GetComponent<Text>();
        missionText.text = properties.title;
    }

    public bool MissionCompleted()
    {
        return new bool();
    }
}
