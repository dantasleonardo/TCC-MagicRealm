using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static LoadingScene Instance;
    public string scene;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    float time;
    public float waitTime;
    public bool withTime;

    public Animator transition;

    public void StartTransition()
    {
        transition.SetBool("Start", true);
    }

    public void LoadScene()
    {
        if (withTime)
            StartCoroutine(LoadSceneAsync());
        else
            StartCoroutine(LoadSceneAsyncExtraTime());
    }

    IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(LoadSceneAsyncExtraTime());
    }

    IEnumerator LoadSceneAsyncExtraTime()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            time += Time.deltaTime;
            yield return null;
        }

        transition.SetBool("Start", false);
        transition.SetBool("End", true);
    }

    IEnumerator Load(AsyncOperation async)
    {
        yield return new WaitForSeconds(waitTime);
        async.allowSceneActivation = true;
    }
}