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
    public void LoadScene(int index) {
        if (withTime)
            StartCoroutine(loadSceneAsync(index));
        else
            StartCoroutine(loadSceneAsyncExtraTime(index));

    }

    IEnumerator loadSceneAsync(int index) {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(loadSceneAsyncExtraTime(index));
    }

    IEnumerator loadSceneAsyncExtraTime(int index) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        while (!asyncLoad.isDone) {
            time += Time.deltaTime;
            Debug.Log(asyncLoad.progress);
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
