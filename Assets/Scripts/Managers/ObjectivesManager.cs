using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
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
    public Text destroyCastleText;
    [TextArea] public string destroyCastleTitle;
    public bool destroyCastleCompleted;
    public bool youLose;

    public string youWin;
    public string youLoseString;


    public Color objectiveCompletedColor;

    public PlayableDirector director;
    public Text gameoverText;

    private void Update() {
        if (GameController.Instance.stonesCollected >= totalStoneResouceCollected && !stoneCompleted) {
            stoneCompleted = true;
            stoneObjectiveText.color = objectiveCompletedColor;
        }
        if (GameController.Instance.woodCollected >= totalWoodResouceCollected && !woodCompleted) {
            woodCompleted= true;
            woodObjectiveText.color = objectiveCompletedColor;
        }
        if(GameController.Instance.magicCastle.life < 1) {
            destroyCastleCompleted = true;
            destroyCastleText.color = objectiveCompletedColor;
        }
        if(GameController.Instance.robotsCastle.life < 1) {
            youLose = true;
        }

        UpdateUI();
        GameOverConditions();
    }

    public void GameOverConditions() {
        if(destroyCastleCompleted && woodCompleted && stoneCompleted) {
            director.gameObject.SetActive(true);
            director.Play();
            gameoverText.text = youWin;
        }
        else if (destroyCastleCompleted) {
            director.gameObject.SetActive(true);
            director.Play();
            gameoverText.text = youWin;
        } else if (youLose) {
            director.gameObject.SetActive(true);
            director.Play();
            gameoverText.text = youLoseString;
        }
    }

    private void UpdateUI() {
        if(GameController.Instance.stonesCollected <= totalStoneResouceCollected) {
            stoneObjectiveText.text = $"{GameController.Instance.stonesCollected}/{totalStoneResouceCollected} {stoneObjectiveTitle}";
            destroyCastleText.text = $"{destroyCastleTitle}";
        }
        if (GameController.Instance.woodCollected <= totalWoodResouceCollected) {
            woodObjectiveText.text = $"{GameController.Instance.woodCollected}/{totalWoodResouceCollected} {woodObjectiveTitle}";
        }
        destroyCastleText.text = $"{destroyCastleTitle}";
    }
}
