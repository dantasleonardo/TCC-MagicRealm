using UnityEngine;
using UnityEngine.UI;

public class GoToLevel : MonoBehaviour
{
    [SerializeField] private string desiredScene;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LoadingScene.Instance.scene = desiredScene;
            LoadingScene.Instance.waitTime = 10f;
            LoadingScene.Instance.StartTransition();
            MainMenuManager.instance.gameObject.SetActive(false);
        });
    }
}