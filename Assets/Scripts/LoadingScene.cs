using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {
    public static LoadingScene Instance;

    private void Awake() {
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

    public void StartTransition() {
        transition.SetBool("Start", true);
    }
    public void LoadScene(string scene) {
        if (withTime)
            StartCoroutine(loadSceneAsync(scene));
        else
            StartCoroutine(loadSceneAsyncExtraTime(scene));

    }

    IEnumerator loadSceneAsync(string scene) {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(loadSceneAsyncExtraTime(scene));
    }

    IEnumerator loadSceneAsyncExtraTime(string scene) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone) {
            time += Time.deltaTime;
            yield return null;
        }
        transition.SetBool("Start", false);
        transition.SetBool("End", true);
    }

    IEnumerator Load(AsyncOperation async) {
        yield return new WaitForSeconds(waitTime);
        async.allowSceneActivation = true;
    }
}
