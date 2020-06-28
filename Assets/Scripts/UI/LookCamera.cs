using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    private void Update()
    {
        if (Camera.main != null)
            transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x,
                Camera.main.transform.parent.gameObject.transform.eulerAngles.y,
                transform.eulerAngles.z);
    }
}
