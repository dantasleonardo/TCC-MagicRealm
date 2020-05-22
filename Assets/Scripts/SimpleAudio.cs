using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAudio : MonoBehaviour
{
    private static SimpleAudio instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
