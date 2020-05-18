using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWaypoints : MonoBehaviour
{

    public static ManagerWaypoints Instance;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public List<Transform> waypoints;
}
