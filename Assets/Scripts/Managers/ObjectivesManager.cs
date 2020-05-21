using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesManager : MonoBehaviour
{
    public int totalStoneResouceCollected;
    public int totalWoodResouceCollected;

    public Text stoneObjectiveText;
    [TextArea] public string stoneObjectiveTitle;
    public bool stoneCompleted;
    public Text woodObjectiveText;
    [TextArea] public string woodObjectiveTitle;
    public bool woodCompleted;

    private void Update() {
        if (GameController.Instance.stonesCollected >= totalStoneResouceCollected)
            stoneCompleted = true;
        if (GameController.Instance.woodCollected >= totalWoodResouceCollected)
            woodCompleted= true;

        UpdateUI();
    }

    private void UpdateUI() {
        if(GameController.Instance.stonesCollected <= totalStoneResouceCollected) {
            stoneObjectiveText.text = $"{GameController.Instance.stonesCollected}/{totalStoneResouceCollected} {stoneObjectiveTitle}";
        }
    }
}
