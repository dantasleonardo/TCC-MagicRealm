using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Magic
{
    public class Crystal : MonoBehaviour, IUnit
    {
        [SerializeField] private int durability = 200;
        [SerializeField] private GameObject mainCrystal;
        [SerializeField] private GameObject brokenCrystal;
        [SerializeField] private bool isDestroyed;
        [SerializeField] private List<GameObject> runePrefab;
        [SerializeField] private float runeTimeCount;
        [SerializeField] private float timeToSpawnRune;


        private void Update()
        {
            GetEnemyDistance();
        }

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
        }

        public bool GetCurrentDurability()
        {
            return durability > 0;
        }

        private void GetEnemyDistance()
        {
            var units = UnitController.Instance.units
                .Where(u => Vector3.Distance(transform.position, u.transform.position) < 3.0f)
                .OrderBy(u => Vector3.Distance(transform.position, u.transform.position)).ToList();
            if (GetCurrentDurability())
            {
                if (units.Count > 0)
                {
                    runeTimeCount += Time.deltaTime;
                    if (runeTimeCount > timeToSpawnRune)
                    {
                        var direction = (units[0].transform.position - transform.position).normalized;
                        var position = transform.position + new Vector3(direction.x, 0.5f, direction.z);
                        var rand = Random.Range(0, runePrefab.Count);
                        Instantiate(runePrefab[rand], position, Quaternion.identity);
                        runeTimeCount = 0.0f;
                    }
                }
            }
            else
            {
                runeTimeCount = 0.0f;
            }
        }
    }
}