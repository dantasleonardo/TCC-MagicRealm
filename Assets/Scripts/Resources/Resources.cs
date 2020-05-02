using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    [SerializeField] private int amountResources;
    [SerializeField] private ResourceType resourceType;

    
    public Dictionary<ResourceType, int> GetResource(int amount) {
        var resourceDesired = new Dictionary<ResourceType, int>();
        if (amountResources >= amount) {
            resourceDesired[resourceType] = amount;
            amountResources -= amount;
        }
        else {
            resourceDesired[resourceType] = amountResources;
            amountResources = 0;
        }

        return resourceDesired;
    }
}

public enum ResourceType
{
    Wood,
    Rock
}
