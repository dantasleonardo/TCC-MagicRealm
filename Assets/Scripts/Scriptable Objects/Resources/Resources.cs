using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public int amountResources;
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private ParticleSystem[] particles;

    
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

    public void SetParticles(bool isActive) {
        if (isActive) {
            foreach (var item in particles) {
                var emission = item.emission;
                emission.rateOverTime = 10.0f;
            }
        }
        else {
            foreach (var item in particles) {
                var emission = item.emission;
                emission.rateOverTime = 0.0f;
            }
        }
    }

    private void Update() {
        if (amountResources <= 0)
            Destroy(this.gameObject);
    }
}

public enum ResourceType
{
    Wood,
    Stone
}
