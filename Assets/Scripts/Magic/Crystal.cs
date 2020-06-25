using UnityEngine;

namespace Magic
{
    public class Crystal : MonoBehaviour, IUnit
    {
        [SerializeField] private int durability = 200;
        [SerializeField] private GameObject mainCrystal;
        [SerializeField] private GameObject brokenCrystal;
        [SerializeField] private bool isDestroyed;


        public void TakeDamage(int damage)
        {
            durability -= damage;
            if (durability < 1)
            {
                brokenCrystal.SetActive(true);
                mainCrystal.SetActive(false);
                if (!isDestroyed)
                {
                    isDestroyed = true;
                    GameController.Instance.crystalsDestroyed += 1;
                }
            }

            Debug.Log($"Durability: {durability}");
        }

        public bool GetCurrentDurability()
        {
            return durability > 0;
        }
    }
}