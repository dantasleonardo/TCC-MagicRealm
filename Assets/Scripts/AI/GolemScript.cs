using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using UnityEngine.AI;

public class GolemScript : IEnemy
{
    [SerializeField] private int life;
    [SerializeField] private Transform spawnAttack;
    [SerializeField] private LifeBar lifeBar;
    [SerializeField] private float disableLifeBar = 3.0f;
    [SerializeField] private Animator animator;
    [SerializeField] float speed;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool isStanding;

    void Start()
    {
        life = properties.totalLife;
        lifeBar.totalValue = life;
        lifeBar.isActive = false;
        lifeBar.gameObject.SetActive(false);
        GameController.Instance.enemies.Add(this.gameObject);
        GetComponent<AI>().Init(properties.distanceSeek, properties.distanceAttack, properties.stopDistance,
            0.0f);
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

        if (GetComponent<AI>().target != null) animator.SetBool("Seeking", true);
        else animator.SetBool("Seeking", false);
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
    
    private IEnumerator DisableLifeBar(int currentLife)
    {
        yield return new WaitForSeconds(disableLifeBar);
        if (currentLife == life)
        {
            lifeBar.BarIsActive(false);
            lifeBar.isActive = false;
        }
    }

    public override void Attack(int typeAttack)
    {
        animator.SetTrigger("Attack");
    }

    public void InstantiateAttack()
    {
        Instantiate(properties.attacksPrefabs[0].bulletPrefab, spawnAttack.position, spawnAttack.rotation);
    }

    [Task]
    private bool IsStanding()
    {
        return isStanding;
    }

    [Task]
    private void RaiseGolem()
    {
        animator.SetTrigger("RaiseGolem");
    }

    public void SetIsStanding()
    {
        isStanding = !isStanding;
        if (isStanding) agent.speed = properties.Speed;
        else agent.speed = 0.0f;
    }

    [Task]
    private bool TargetIsBase()
    {
        var target = GetComponent<AI>().target;
        if (target != null)
        {
            if (target.CompareTag("baseGolem"))
            {
                Task.current.Succeed();
                return true;
            }
            else
            {
                Task.current.Succeed();
                return false;
            }
        }

        return false;
    }
}