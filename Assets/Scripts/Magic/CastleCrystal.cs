using UnityEngine;

namespace Magic
{
    public class CastleCrystal : MonoBehaviour
    {
        [SerializeField] private GameObject mainCrystal;
        [SerializeField] private GameObject brokenCrystal;
        [SerializeField] private bool useDestroy;
        [SerializeField] private float timeToDestroy = 5.0f;

        public void DestroyCrystal()
        {
            brokenCrystal.SetActive(true);
            mainCrystal.SetActive(false);
            if (useDestroy)
                Destroy(this.gameObject, timeToDestroy);
        }
    }
}