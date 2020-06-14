using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class MageScript : MonoBehaviour, IEnemy {
    public Mage properties;
    [SerializeField] private Transform spawnAttack;
    [SerializeField] private LifeBar lifeBar;
    [SerializeField] private float disableLifeBar = 3.0f;
    private int life;



    public void Attack(int typeAttack) {
        StopAllCoroutines();

        if (typeAttack == 0) {
            Instantiate(properties.attacksPrefabs[0].bulletPrefab, spawnAttack.position, spawnAttack.rotation);
        }
    }

    public void TakeDamage(int damage) {
        life -= damage;

        if (!lifeBar.isActive) {
            lifeBar.isActive = true;
            lifeBar.BarIsActive(true);
        }
        lifeBar.UpdateBar(life);
        StartCoroutine(DisableLifeBar());
    }

    private void Update() {
        float fillAmount = lifeBar.foregroundBar.fillAmount;

        if(life <= 0 && fillAmount <= 0.0f) {
            GameController.Instance.Enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DisableLifeBar() {
        yield return new WaitForSeconds(disableLifeBar);
        lifeBar.BarIsActive(false);
        lifeBar.isActive = false;
    }

    void Start() {
        life = properties.totalLife;
        lifeBar.totalValue = life;
        GameController.Instance.Enemies.Add(this.gameObject);
        GetComponent<AI>().Init(properties.distanceSeek, properties.distanceAttack, properties.stopDistance, properties.Speed);
    }
}
