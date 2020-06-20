using System;
using UnityEngine;
using UnityEngine.AI;

public class AttackUnitScript : Robot
{
    [Header("AttackUnit properties")] [SerializeField]
    private string fileNameOfUnit;

    public AttackUnit attackUnitProperties;

    [SerializeField] private float attakDistance;
    [SerializeField] private float distanceSeek = 2.25f;
    [SerializeField] private float firerateAttack;
    [SerializeField] private float turningSpeed;

    [SerializeField] private string fileNameOfBullet;
    [SerializeField] private Bullet bulletProperties;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] spawnBulletPosition;
    private int nextSpawn;

    [Header("Animations")] [SerializeField]
    private float distanceStopAnimationPlay = 0.2f;

    public bool attackWithAnimation = false;

    private Animator animator;

    [SerializeField] private GameObject currentTarget;
    [SerializeField] private float currentTimeToFire;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void InitItems()
    {
        bulletProperties = UnityEngine.Resources.Load<Bullet>(fileNameOfBullet);
        agent = GetComponent<NavMeshAgent>();

        if (bulletProperties != null)
        {
            bulletPrefab = bulletProperties.bulletPrefab;
        }

        if (attackUnitProperties != null)
        {
            life = attackUnitProperties.life;
            speedMovement = attackUnitProperties.speedMovement;
            attakDistance = attackUnitProperties.attackDistace;
            firerateAttack = attackUnitProperties.firerateAttack;
            turningSpeed = attackUnitProperties.turningSpeed;
            agent.speed = speedMovement;
            robotType = attackUnitProperties.RobotType;
            firerateAttack = attackUnitProperties.firerateAttack;
            lifeBar.totalValue = life;
        }
    }

    public override void Action(Vector3 target, GameObject targetObject = null)
    {
        print($"Hit: {targetObject.tag}");
        if (targetObject.CompareTag("Mages"))
        {
            agent.stoppingDistance = attakDistance;
            currentTarget = targetObject;
        }
        else if (targetObject.CompareTag("Ground"))
        {
            agent.stoppingDistance = 0.0f;
            currentTarget = null;
            MoveTo(target);
        }
    }

    private void Update()
    {
        GetEnemyDistance();
        var speed = Vector3.Project(agent.desiredVelocity, transform.forward).magnitude;
        animator.SetFloat("Speed", speed);
        if (speed < 0.1f && currentTarget != null)
        {
            RaycastHit hit;
            LookTarget();
            var position = transform.position + new Vector3(0.0f, 0.3f, 0.0f);
            if (Physics.Raycast(position, transform.forward, out hit, 50.0f))
            {
                if (hit.collider.CompareTag("Mages"))
                {
                    // transform.LookAt(currentTarget.transform);
                    LookTarget();
                    currentTimeToFire += Time.deltaTime;

                    if (currentTimeToFire > firerateAttack)
                    {
                        if (attackWithAnimation)
                            animator.SetTrigger("Attacking");
                        else BulletInstantiate();
                        currentTimeToFire = 0.0f;
                    }
                }
            }
        }
        else if (currentTarget != null && currentTarget.CompareTag("Mages"))
        {
            MoveTo(currentTarget.transform.position);
        }
    }

    public void BulletInstantiate()
    {
        if (spawnBulletPosition.Length > 1)
        {
            var bullet = Instantiate(bulletPrefab, spawnBulletPosition[nextSpawn].position, transform.rotation);
            bullet.transform.forward = spawnBulletPosition[nextSpawn].forward;
            nextSpawn = nextSpawn >= 1 ? 0 : 1;
        }
        else
        {
            var bullet = Instantiate(bulletPrefab, spawnBulletPosition[0].position, transform.rotation);
            bullet.transform.forward = spawnBulletPosition[0].forward;
        }
    }

    private bool WithinReach()
    {
        bool isTrue = Vector3.Distance(transform.position, agent.destination) <= attakDistance;
        return isTrue;
    }

    private Quaternion LookTarget()
    {
        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);

        return lookRotation;
    }

    public void GetEnemyDistance()
    {
        if (currentTarget == null)
        {
            GameController.Instance.Enemies.ForEach(u =>
            {
                if (Vector3.Distance(transform.position, u.transform.position) <= distanceSeek)
                {
                    currentTarget = u;
                }
            });
        }
    }
}