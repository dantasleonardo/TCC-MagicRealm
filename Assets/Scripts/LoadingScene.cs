using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    float time;
    public float waitTime;
    public void LoadScene() {
        StartCoroutine(loadSceneAsyncExtraTime(2));
    }

    IEnumerator loadSceneAsync(int index) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        while (!asyncLoad.isDone) {
            time += Time.deltaTime;
            Debug.Log($"Time: {time}");
            yield return null;
        }
    }

    IEnumerator loadSceneAsyncExtraTime(int index) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        while (!asyncLoad.isDone) {
            time += Time.deltaTime;
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
        //StartCoroutine(Load(asyncLoad));
    }

    IEnumerator Load(AsyncOperation async) {
        yield return new WaitForSeconds(waitTime);
        async.allowSceneActivation = true;
    }
}
