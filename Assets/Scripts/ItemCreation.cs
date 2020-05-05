﻿using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class ItemCreation
{
    public float currentTimeToCreate = 0.0f;
    public float timeToCreate = 2.0f;
    public int rockCost;
    public int woodCost;
    public RobotType robotType;
    
    public GameObject prefab;
}
