using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAi : MonoBehaviour
{
    public BaseAI baseAi;
    private Animator animator;

    [SerializeField] private float speed = 1.5f;

    [Header("Variables")]
    [SerializeField] private bool seek;
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


    private void Start() {
        baseAi = GetComponent<BaseAI>();
        animator = GetComponent<Animator>();
        baseAi.agent.speed = speed;
    }

    private void Update() {
    }

    public void UpdateAnimation(bool seek, bool waypoint, bool flee, bool attacking) {
        animator.SetBool("Seek", seek);
        animator.SetBool("Waypoint", waypoint);
        animator.SetBool("Flee", flee);
        animator.SetBool("Attacking", attacking);
    }
}
