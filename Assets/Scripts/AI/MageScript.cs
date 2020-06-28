using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MageScript : IEnemy
{
    [SerializeField] private Transform spawnAttack;
    [SerializeField] private LifeBar lifeBar;
    [SerializeField] private float disableLifeBar = 3.0f;
    [SerializeField] private Animator animator;
    [SerializeField] float speed;
    [SerializeField] private NavMeshAgent agent;
    private int life;


    public override void Attack(int typeAttack)
    {
        StopAllCoroutines();
        animator.SetTrigger("Attacking");
        animator.SetInteger("typeAttack", typeAttack);
    }

    public void AttackInstantiate()
    {
        int typeAttack = animator.GetInteger("typeAttack");
        Instantiate(properties.attacksPrefabs[typeAttack].bulletPrefab, spawnAttack.position, spawnAttack.rotation);
    }

    public override void TakeDamage(int damage)
    {
        life -= damage;

        if (!lifeBar.isActive)
        {
            lifeBar.isActive = true;
            lifeBar.BarIsActive(true);
        }

        lifeBar.UpdateBar(life);
        var disableBar = StartCoroutine(DisableLifeBar(life));
    }

    private void Update()
    {
        float fillAmount = lifeBar.foregroundBar.fillAmount;
        speed = Vector3.Project(agent.desiredVelocity, transform.forward).magnitude;
        animator.SetFloat("Speed", speed);

        if (life <= 0 && fillAmount <= 0.0f)
        {
            GameController.Instance.enemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DisableLifeBar(int currentLife)
    {
        yield return new WaitForSeconds(disableLifeBar);
        if (currentLife == life)
        {
            lifeBar.BarIsActive(false);
            lifeBar.isActive = false;
        }
    }

    void Start()
    {
        life = properties.totalLife;
        lifeBar.totalValue = life;
        GameController.Instance.enemies.Add(this.gameObject);
        GetComponent<AI>().Init(properties.distanceSeek, properties.distanceAttack, properties.stopDistance,
            properties.Speed);
    }
}