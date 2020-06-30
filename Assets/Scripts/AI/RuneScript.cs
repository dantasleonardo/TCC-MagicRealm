using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RuneScript : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private bool markedTarget;
    [SerializeField] private float distanceExplosion;
    private NavMeshAgent agent;
    private Animator animator;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = speed;
    }

    private void Update()
    {
        if (!markedTarget)
            GetTarget();
        if (markedTarget && target == null)
            animator.SetTrigger("Boom");
        if (target != null)
        {
            agent.SetDestination(target.position);
            var currentSpeed = Vector3.Project(agent.desiredVelocity, transform.forward).magnitude;
            Debug.Log($"agent: {agent.remainingDistance}");
            var distance = Vector3.Distance(transform.position, target.position);
            if (distance <= distanceExplosion)
                animator.SetTrigger("Boom");
            else
                agent.SetDestination(target.position);
        }
    }

    private void GetTarget()
    {
        var units = UnitController.Instance.units
            .Where(u => Vector3.Distance(transform.position, u.transform.position) < 10f)
            .OrderBy(u => Vector3.Distance(transform.position, u.transform.position)).ToList();

        if (units.Count >= 1)
        {
            target = units[0].transform;
            markedTarget = true;
        }
    }

    public void HeIsBeenHit()
    {
        animator.SetTrigger("Boom");
    }

    public void DamageAndDestroy()
    {
        if (target != null)
        {
            var robots =
                UnitController.Instance.units.Where(u =>
                    Vector3.Distance(transform.position, u.transform.position) < 1.0f).ToList();
            foreach (var robot in robots)
            {
                var r = robot.GetComponent<Robot>();
                r.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}