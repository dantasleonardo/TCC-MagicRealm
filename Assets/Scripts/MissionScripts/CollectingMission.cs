using System;
using UnityEngine;
using UnityEngine.UI;

namespace MissionScripts
{
    public class CollectingMission : MonoBehaviour, IMission
    {
        [SerializeField] private Mission properties;
        [SerializeField] private Text missionText;

        public void Init(Mission mission)
        {
            properties = mission;
            missionText = GetComponent<Text>();
            missionText.text = properties.title;
        }

        [SerializeField]
        public bool MissionCompleted()
        {
            return new bool();
        }
    }
}