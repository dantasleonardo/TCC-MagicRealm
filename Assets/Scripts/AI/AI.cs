using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class AI : MonoBehaviour {
    public NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceSeek;
    [SerializeField] private float distanceAttack;
    [SerializeField] private float stopDistance;
    [SerializeField] private float turningSpeed = 2.5f;
    private int currentWaypoint;

    [SerializeField] private float timeFirerate = 2.0f;
    [SerializeField] private float timeCount = 0.0f;

    public void Init(float distSeek, float distAttack, float stopDist, float speed) {
        agent = GetComponent<NavMeshAgent>();
        distanceAttack = distAttack;
        distanceSeek = distSeek;
        stopDistance = stopDist;
        agent.stoppingDistance = stopDistance;
        agent.speed = speed;
    }

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Seek(Vector3 target) {
        agent.SetDestination(target);
    }

    [Task]
    public void Waypoint() {
        agent.stoppingDistance = stopDistance;
        int count;
        do {
            count = Random.Range(0, ManagerWaypoints.Instance.waypoints.Count);
        } while (count == currentWaypoint);
        Vector3 target = ManagerWaypoints.Instance.waypoints[count].position;
        Seek(target);
        Task.current.Succeed();
    }

    [Task]
    public void AgentArrivedDestination() {
        if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance) {
            Task.current.Succeed();
        }
    }

    [Task]
    public void AgentArrivedDestinationWithGetDistance() {
        if (target == null) {
            distanceSeek = gameObject.GetComponent<MageScript>().properties.distanceSeek;
            UnitController.Instance.units.ForEach(u => {
                if (Vector3.Distance(transform.position, u.transform.position) <= distanceSeek) {
                    target = u.transform;
                    Seek(target.position);
                    Task.current.Succeed();
                }
            });
        }
        if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance) {
            Task.current.Succeed();
        }
    }

    [Task]
    public bool HaveTarget() {
        if (target != null)
            return true;
        else
            return false;
    }

    [Task]
    public void GetEnemyDistance() {
        if (target == null) {
            distanceSeek = gameObject.GetComponent<MageScript>().properties.distanceSeek;
            UnitController.Instance.units.ForEach(u => {
                if (Vector3.Distance(transform.position, u.transform.position) <= distanceSeek) {
                    target = u.transform;
                    Seek(target.position);
                    Task.current.Succeed();
                }
            });
        }
        Task.current.Succeed();
    }

    [Task]
    public void SeekTarget() {
        agent.stoppingDistance = distanceAttack;
        Seek(target.position);
        Task.current.Succeed();
    }

    [Task]
    public bool DistanceAttack() {
        if (target != null) {
            if (Vector3.Distance(transform.position, target.position) <= agent.stoppingDistance)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    [Task]
    public void Attack() {
        LookTarget();
        timeCount += Time.deltaTime;
        if(timeCount > timeFirerate) {
            var attack = GetComponent<IEnemy>();
            attack.Attack(0);
            timeCount = 0.0f;
        }
        Task.current.Succeed();
    }

    public void LookTarget() {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);
    }
}
