using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuePanel : MonoBehaviour
{
    [SerializeField] private List<Button> levels;

    private void OnEnable()
    {
        var levelsCompleted = SaveSystem.SaveSystem.Load().levelsCompleted;
        levels[0].interactable = true;
        for (int i = 1; i < levelsCompleted.Length; i++)
        {
            levels[i].interactable = levelsCompleted[i];
        }
    }
}