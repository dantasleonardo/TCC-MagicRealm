using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Robot : UnitScript, IUnit
{
    [Header("Robots Properties")] [SerializeField]
    protected int life;

    [SerializeField] protected float speedMovement = 2.0f;
    public RobotType robotType;
    [SerializeField] protected LifeBar lifeBar;
    [SerializeField] private float disableLifeBar = 3.0f;

    protected NavMeshAgent agent;

    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public void TakeDamage(int damage)
    {
        StopAllCoroutines();
        life -= damage;

        if (!lifeBar.isActive)
        {
            lifeBar.isActive = true;
            lifeBar.BarIsActive(true);
        }

        lifeBar.UpdateBar(life);
        StartCoroutine(DisableLifeBar());
    }

    private void FixedUpdate()
    {
        float fillAmount = lifeBar.foregroundBar.fillAmount;

        if (life <= 0 && fillAmount <= 0.0f)
        {
            UnitController.Instance.units.Remove(this);
            if (GetComponent<AttackUnitScript>() != null)
                GameController.Instance.attackUnits.Remove(gameObject);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DisableLifeBar()
    {
        yield return new WaitForSeconds(disableLifeBar);
        lifeBar.BarIsActive(false);
        lifeBar.isActive = false;
    }
}


public enum RobotType
{
    Attack,
    Gatherer
}