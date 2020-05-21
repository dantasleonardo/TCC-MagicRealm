using UnityEngine;
using UnityEngine.AI;

public class AttackUnitScript : Robot
{
    [Header("AttackUnit properties")] [SerializeField]
    private string fileNameOfUnit;

    public AttackUnit attackUnitProperties;

    [SerializeField] private float attakDistance;
    [SerializeField] private float firerateAttack;
    [SerializeField] private float turningSpeed;

    [SerializeField] private string fileNameOfBullet;
    [SerializeField] private Bullet bulletProperties;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnBulletPosition;

    [Header("Animations")] [SerializeField]
    private float distanceStopAnimationPlay = 0.2f;

    private Animator animator;

    [SerializeField] private GameObject currentTarget;
    [SerializeField] private float nextTimeToFire;

    [Header("Actions")] public bool idle = true;
    public bool inMovement;
    public bool attacking = false;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public override void InitItems() {
        bulletProperties = UnityEngine.Resources.Load<Bullet>(fileNameOfBullet);
        agent = GetComponent<NavMeshAgent>();

        if (bulletProperties != null) {
            bulletPrefab = bulletProperties.bulletPrefab;
        }

        if (attackUnitProperties != null) {
            life = attackUnitProperties.life;
            speedMovement = attackUnitProperties.speedMovement;
            attakDistance = attackUnitProperties.attackDistace;
            firerateAttack = attackUnitProperties.firerateAttack;
            turningSpeed = attackUnitProperties.turningSpeed;
            agent.speed = speedMovement;
            robotType = attackUnitProperties.RobotType;
        }
    }

    public override void Action(Vector3 target, GameObject targetObject = null) {
        print($"Hit: {targetObject.tag}");
        if (targetObject.CompareTag("Mages")) {
            agent.stoppingDistance = attakDistance;
            currentTarget = targetObject;
            ActiveMovement();
            if (!WithinReach())
                ActiveMovement();
        }
        else if (targetObject.CompareTag("Ground")) {
            agent.stoppingDistance = 0.0f;
            attacking = false;
            currentTarget = null;
            MoveTo(target);
            ActiveMovement();
        }
    }

    private void ActiveMovement() {
        idle = false;
        inMovement = true;

        animator.SetBool("inMovement", inMovement);
        animator.SetBool("Idle", idle);
    }

    private void Update() {
        if (inMovement) {
            if (Vector3.Distance(agent.destination, transform.position) < distanceStopAnimationPlay) {
                inMovement = false;
                idle = true;
                animator.SetBool("inMovement", inMovement);
                animator.SetBool("Idle", idle);
            }
        }

        if (attacking && currentTarget != null) {
            // transform.LookAt(currentTarget.transform);
            var lookRotation = LookTarget();


            if (Time.time > nextTimeToFire) {
                var bullet = Instantiate(bulletPrefab, spawnBulletPosition.position, transform.rotation);
                Debug.Log($"InstantiateBullet: {bullet.name}");
                bullet.transform.forward = spawnBulletPosition.forward;
                nextTimeToFire = Time.time + (1f / firerateAttack);
            }
            else {
                MoveTo(currentTarget.transform.position);
                inMovement = true;
                idle = false;
                animator.SetBool("inMovement", inMovement);
                animator.SetBool("Idle", idle);
            }
        }

        if (WithinReach() && currentTarget != null) {
            attacking = true;
            idle = true;
            inMovement = false;
            animator.SetBool("inMovement", inMovement);
            animator.SetBool("Idle", idle);
        }
        else if (currentTarget != null && currentTarget.CompareTag("Mages")) {
            attacking = false;
            MoveTo(currentTarget.transform.position);
        }
    }

    private bool WithinReach() {
        bool isTrue = Vector3.Distance(transform.position, agent.destination) <= attakDistance;
        return isTrue;
    }

    private Quaternion LookTarget() {
        Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);

        return lookRotation;
    }
}