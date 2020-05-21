using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageScript : MonoBehaviour, IEnemy
{
    [SerializeField] private Mage properties;
    [SerializeField] private Transform spawnAttack;


    private int life;


    public void Attack(int typeAttack) {
        if(typeAttack == 0) {
            Instantiate(properties.attacksPrefabs[0].bulletPrefab, spawnAttack.position, spawnAttack.rotation);
        }
    }

    public void TakeDamage(int damage) {
        life -= damage;
        if (life <= 0)
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        life = properties.totalLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
