using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountGatherer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI textCount;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Camera.main != null)
            transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x,
                Camera.main.transform.parent.gameObject.transform.eulerAngles.y,
                transform.eulerAngles.z);
    }

    public void SetCountValue(int value)
    {
        textCount.text = $"+{value}";
    }

    public void DestroyCount()
    {
        Destroy(gameObject);
    }
}