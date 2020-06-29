using LocalizationSystem;
using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private GameObject continuePanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject newGameWarningPopup;
    [SerializeField] private Button continueWarningButton;
    [SerializeField] private string loadScene = "Nivel1";

    void Start()
    {
        if (SaveSystem.SaveSystem.Load() != null)
            continueButton.gameObject.SetActive(true);
        else
            continueButton.gameObject.SetActive(false);

        continueButton.onClick.AddListener(() =>
        {
            continuePanel.SetActive(true);
            mainMenuPanel.SetActive(false);
        });
        newGameButton.onClick.AddListener(() =>
        {
            if (SaveSystem.SaveSystem.Load() != null)
                newGameWarningPopup.SetActive(true);
            else
                NewGame();
        });

        continueWarningButton.onClick.AddListener(() =>
        {
            NewGame();
            newGameWarningPopup.SetActive(false);
        });
    }

    private void NewGame()
    {
        var user = new User(LocalizationManager.instance.GetLanguageKey(), GameManager.instance.volume,
            new bool[] {true, true, false});
        SaveSystem.SaveSystem.Save(user);
        LoadingScene.Instance.scene = loadScene;
        LoadingScene.Instance.StartTransition();
        this.gameObject.SetActive(false);
    }
}