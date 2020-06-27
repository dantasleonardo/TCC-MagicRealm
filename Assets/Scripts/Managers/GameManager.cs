using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool startedKeyPressed;

    public static GameManager instance;
    public float volume;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}