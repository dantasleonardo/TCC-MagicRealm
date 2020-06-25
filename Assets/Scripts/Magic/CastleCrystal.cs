using UnityEngine;

namespace Magic
{
    public class CastleCrystal : MonoBehaviour
    {
        [SerializeField] private GameObject mainCrystal;
        [SerializeField] private GameObject brokenCrystal;

        public void DestroyCrystal()
        {
            brokenCrystal.SetActive(true);
            mainCrystal.SetActive(false);
            Destroy(this.gameObject, 5.0f);
        }
    }
}
