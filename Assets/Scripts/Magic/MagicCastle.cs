using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCastle : MonoBehaviour,IEnemy
{
    public int life;
    [SerializeField] private LifeBar lifeBar;

    public float timeSpawn;
    public float currentTime;
    public float ermegencySpawnTime;

    public GameObject magePrefab;
    public Transform spawnPoint;


    float distanceSeek;
    private void Start() {
        lifeBar.totalValue = life;
    }

    public void Attack(int typeAttack) {
        
    }

    public void TakeDamage(int damage) {
        life -= damage;
        lifeBar.UpdateBar((float)life);
    }

    private void Update() {
        if(GameController.Instance.Enemies.Count < 4) {
            currentTime += Time.deltaTime;
            if (currentTime > ermegencySpawnTime) {
                Instantiate(magePrefab, spawnPoint.position, spawnPoint.rotation);
                currentTime = 0.0f;
            }
        } else if(GameController.Instance.Enemies.Count > 3 && GameController.Instance.Enemies.Count < 8) {
            currentTime += Time.deltaTime;
            if(currentTime > timeSpawn) {
                Instantiate(magePrefab, spawnPoint.position, spawnPoint.rotation);
                currentTime = 0.0f;
            }
        }
    }
}
