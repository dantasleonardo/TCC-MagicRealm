using Magic;
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
    [SerializeField] private AudioSource fireSound;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] spawnBulletPosition;
    private int nextSpawn;

    [Header("Animations")] [SerializeField]
    private float distanceStopAnimationPlay = 0.2f;

    public bool attackWithAnimation = false;

    private Animator animator;

    [SerializeField] private GameObject currentTarget;
    [SerializeField] private GameObject groundTarget;
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

        GameController.Instance.attackUnits.Add(gameObject);
    }

    public override void Action(Vector3 target, GameObject targetObject = null)
    {
        print($"Hit: {targetObject.tag}");
        if (targetObject.CompareTag("Mages") || targetObject.CompareTag("Crystal"))
        {
            agent.stoppingDistance = attakDistance;
            currentTarget = targetObject;
            MoveTo(target);
        }
        else if (targetObject.CompareTag("Ground"))
        {
            currentTarget = null;
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = 0.1f;
            var go = new GameObject();
            go.transform.position = target;
            groundTarget = go;
            MoveTo(target);
        }
    }

    private void Update()
    {
        GetEnemyDistance();
        var speed = Vector3.Project(agent.desiredVelocity, transform.forward).magnitude;
        animator.SetFloat("Speed", speed);
        if (groundTarget != null)
        {
            Debug.Log($"Distance Ground point: {(groundTarget.transform.position - transform.position).magnitude}");
            float groundDistance = 1;
            switch (attackUnitProperties.nameUnit)
            {
                case "Soldier":
                    groundDistance = 0.05f;
                    break;
                case "Cannon":
                    groundDistance = 0.16f;
                    break;
            }

            if ((groundTarget.transform.position - transform.position).magnitude < groundDistance)
            {
                groundTarget = null;
            }
        }

        if (currentTarget != null)
        {
            if (speed < 0.1f)
            {
                if (currentTarget.CompareTag("Crystal"))
                {
                    currentTarget = currentTarget.GetComponent<Crystal>().GetCurrentDurability()
                        ? currentTarget
                        : null;
                    if (currentTarget == null)
                        return;
                }


                RaycastHit hit;
                LookTarget();
                var position = transform.position + new Vector3(0.0f, 0.3f, 0.0f);
                if (Physics.Raycast(position, transform.forward, out hit,
                    attackUnitProperties.attackDistace + 2.0f))
                {
                    if (hit.collider.CompareTag("Mages") || hit.collider.CompareTag("Crystal"))
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

                Debug.Log(
                    $"Robo {gameObject.name} distance target: {Vector3.Distance(transform.position, currentTarget.transform.position)}");
                if (Vector3.Distance(transform.position, currentTarget.transform.position) > attakDistance)
                {
                    Debug.Log($"Robo {gameObject.name} etrou no if");
                    agent.stoppingDistance = attakDistance;
                    agent.isStopped = false;
                    MoveTo(currentTarget.transform.position);
                }
            }

            if (Vector3.Distance(transform.position, currentTarget.transform.position) <= attakDistance)
            {
                agent.isStopped = true;
            }
            else if (currentTarget.CompareTag("Mages"))
            {
                agent.isStopped = false;
                agent.stoppingDistance = attakDistance;
                MoveTo(currentTarget.transform.position);
            }
            else if (currentTarget.CompareTag("Crystal"))
            {
                agent.isStopped = false;
                agent.stoppingDistance = attakDistance;
                MoveTo(currentTarget.transform.position);
            }
        }
        else
        {
            agent.isStopped = false;
        }
    }

    public void BulletInstantiate()
    {
        if (spawnBulletPosition.Length > 1)
        {
            var bullet = Instantiate(bulletPrefab, spawnBulletPosition[nextSpawn].position, transform.rotation);
            bullet.transform.forward = spawnBulletPosition[nextSpawn].forward;
            nextSpawn = nextSpawn >= 1 ? 0 : 1;
            if (fireSound)
                fireSound.Play();
        }
        else
        {
            var bullet = Instantiate(bulletPrefab, spawnBulletPosition[0].position, transform.rotation);
            bullet.transform.forward = spawnBulletPosition[0].forward;
            if (fireSound)
                fireSound.Play();
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
        if (groundTarget == null)
        {
            if (currentTarget == null)
            {
                GameController.Instance.enemies.ForEach(u =>
                {
                    if (Vector3.Distance(transform.position, u.transform.position) <= distanceSeek)
                    {
                        currentTarget = u;
                    }
                });
            }
        }
    }
}