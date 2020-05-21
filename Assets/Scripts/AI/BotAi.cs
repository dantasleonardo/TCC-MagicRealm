using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAi : MonoBehaviour
{
    public IEnemy enemy;
    public BaseAI baseAi;
    public Animator animator;

    [SerializeField] private float speed = 1.5f;

    [Header("Variables")]
    public bool seek;
    [SerializeField] private bool wander;
    [SerializeField] private bool flee;
    [SerializeField] private bool attacking;
    public Transform target;

    [Header("Decisions")]
    public float distanceSeek;
    public float distanceAttack;
    [Range(0.0f, 1.0f)]
    public float fleeProbabillity;
    public List<UnitScript> units = new List<UnitScript>();

    [Header("Other")]
    private Vector3 lastPosition;
    public float currentSpeed;
    [SerializeField] private float turningSpeed = 2.5f;


    private void Start() {
        baseAi = GetComponent<BaseAI>();
        animator = GetComponent<Animator>();
        baseAi.agent.speed = speed;
        enemy = GetComponent<IEnemy>();
    }

    private void Update() {
        currentSpeed = Mathf.Lerp(speed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
        lastPosition = transform.position;
        animator.SetFloat("Speed", currentSpeed);
    }

    public void UpdateAnimation(bool seek, bool waypoint, bool flee, bool attacking) {
        animator.SetBool("Seek", seek);
        animator.SetBool("Waypoint", waypoint);
        animator.SetBool("Flee", flee);
        animator.SetBool("Attacking", attacking);
    }

    public Quaternion LookTarget() {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);

        return lookRotation;
    }
}
