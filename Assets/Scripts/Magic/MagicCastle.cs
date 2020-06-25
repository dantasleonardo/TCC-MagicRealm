using System.Linq;
using Magic;
using UnityEngine;

public class MagicCastle : IEnemy
{
    public int life;
    [SerializeField] private LifeBar lifeBar;

    [SerializeField] private float timeSpawn;
    [SerializeField] private float currentTime;
    [SerializeField] private float emergencySpawnTime;
    [SerializeField] private int minMageInMap = 4;
    [SerializeField] private int maxMageInMap = 8;

    [SerializeField] private GameObject magePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CastleCrystal[] crystalsOfCastle;


    float distanceSeek;

    private void Start()
    {
        lifeBar.totalValue = life;
    }

    public override void Attack(int typeAttack)
    {
    }

    public override void TakeDamage(int damage)
    {
        life -= damage;
        lifeBar.UpdateBar((float) life);
        var lifePercent = (float) life / lifeBar.totalValue;
        if (lifePercent <= 0.0f)
        {
            crystalsOfCastle[3].DestroyCrystal();
        }
        else if (lifePercent < 0.25f)
        {
            crystalsOfCastle[2].DestroyCrystal();
        }
        else if (lifePercent < 0.5f)
        {
            crystalsOfCastle[1].DestroyCrystal();
        }
        else if (lifePercent < 0.75f)
        {
            crystalsOfCastle[0].DestroyCrystal();
        }
    }

    private void Update()
    {
        var lifePercent = (float) life / lifeBar.totalValue;
        if (lifePercent <= 0.0f)
        {
            crystalsOfCastle[3].DestroyCrystal();
        }
        else if (lifePercent < 0.25f)
        {
            crystalsOfCastle[2].DestroyCrystal();
        }
        else if (lifePercent < 0.5f)
        {
            crystalsOfCastle[1].DestroyCrystal();
        }
        else if (lifePercent < 0.75f)
        {
            crystalsOfCastle[0].DestroyCrystal();
        }

        if (GameController.Instance.enemies.Count < minMageInMap)
        {
            currentTime += Time.deltaTime;
            if (currentTime > emergencySpawnTime)
            {
                Instantiate(magePrefab, spawnPoint.position, spawnPoint.rotation);
                currentTime = 0.0f;
            }
        }
        else if (GameController.Instance.enemies.Count >= minMageInMap)
        {

            var attackUnits = GameController.Instance.attackUnits.Count;
            Debug.Log($"quantidade de unidade de ataque : {attackUnits}");
            if (maxMageInMap > attackUnits && GameController.Instance.enemies.Count <= maxMageInMap)
            {
                currentTime += Time.deltaTime;
                if (currentTime > timeSpawn)
                {
                    Instantiate(magePrefab, spawnPoint.position, spawnPoint.rotation);
                    currentTime = 0.0f;
                }
            }
            else if (GameController.Instance.enemies.Count < attackUnits)
            {
                currentTime += Time.deltaTime;
                if (currentTime > timeSpawn)
                {
                    Instantiate(magePrefab, spawnPoint.position, spawnPoint.rotation);
                    currentTime = 0.0f;
                }
            }
        }
    }
}