﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;
using Random = UnityEngine.Random;

public class AI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    [SerializeField] private float distanceSeek;
    [SerializeField] private float distanceAttack;
    [SerializeField] private float distanceAttackCastle = 4.0f;
    [SerializeField] private float stopDistance;
    [SerializeField] private float turningSpeed = 2.5f;
    private int currentWaypoint;

    [SerializeField] private float timeFirerate = 2.0f;
    [SerializeField] private float timeCount = 0.0f;

    public void Init(float distSeek, float distAttack, float stopDist, float speed)
    {
        agent = GetComponent<NavMeshAgent>();
        distanceAttack = distAttack;
        distanceSeek = distSeek;
        stopDistance = stopDist;
        agent.stoppingDistance = stopDistance;
        agent.speed = speed;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Seek(Vector3 target)
    {
        agent.SetDestination(target);
    }

    private void Update()
    {
        if (target != null)
        {
            if (target.GetComponent<RobotsCastle>() != null)
            {
                agent.stoppingDistance = distanceAttackCastle;
            }
            else
            {
                agent.stoppingDistance = distanceAttack;
            }
        }
    }

    [Task]
    public void Waypoint()
    {
        agent.stoppingDistance = stopDistance;
        int count;
        do
        {
            count = Random.Range(0, ManagerWaypoints.Instance.waypoints.Count);
        } while (count == currentWaypoint);

        Vector3 target = ManagerWaypoints.Instance.waypoints[count].position;
        Seek(target);
        Task.current.Succeed();
    }

    [Task]
    public void AgentArrivedDestination()
    {
        if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public void AgentArrivedDestinationWithGetDistance()
    {
        if (target == null)
        {
            distanceSeek = gameObject.GetComponent<MageScript>().properties.distanceSeek;
            UnitController.Instance.units.ForEach(u =>
            {
                if (Vector3.Distance(transform.position, u.transform.position) <= distanceSeek)
                {
                    target = u.transform;
                    Seek(target.position);
                    Task.current.Succeed();
                }
            });
        }

        if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public bool HaveTarget()
    {
        if (target != null)
            return true;
        else
            return false;
    }

    [Task]
    public void GetEnemyDistance()
    {
        if (target == null)
        {
            distanceSeek = gameObject.GetComponent<IEnemy>().properties.distanceSeek;
            UnitController.Instance.units.ForEach(u =>
            {
                if (Vector3.Distance(transform.position, u.transform.position) <= distanceSeek)
                {
                    target = u.transform;
                    Seek(target.position);
                    Task.current.Succeed();
                }
            });
        }

        Task.current.Succeed();
    }

    [Task]
    public void SeekTarget()
    {
        agent.stoppingDistance = distanceAttack;
        Seek(target.position);
        Task.current.Succeed();
    }

    [Task]
    public bool DistanceAttack()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                return true;
            }
            else
            {
                agent.isStopped = false;
                return false;
            }
        }
        else
            return false;
    }

    [Task]
    public void Attack()
    {
        LookTarget();
        
        timeCount += Time.deltaTime;
        if (timeCount > timeFirerate)
        {
            var attack = GetComponent<IEnemy>();
            attack.Attack(0);
            timeCount = 0.0f;
        }

        Task.current.Succeed();
    }

    [Task]
    public bool ArrivedDestination()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 0.1f)
        {
            target = null;
            Task.current.Succeed();
            return true;
        }
        else
        {
            Task.current.Succeed();
            return false;
        }
    }

    [Task]
    public void CastleIsTarget()
    {
        if (Random.Range(0.0f, 100.0f) < 10.0f)
        {
            distanceSeek = 100.0f;
            target = GameController.Instance.robotsCastle.transform;
        }

        Task.current.Succeed();
    }

    public void LookTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);
    }

    [Task]
    public void CheckResources()
    {
        Debug.Log($"{gameObject.name}  Verificou Resources");
        if (GameController.Instance.resources[ResourceType.Stone] > 150 ||
            GameController.Instance.resources[ResourceType.Stone] > 150)
        {
            Debug.Log($"{gameObject.name}  está com o castelo como alvo");
            agent.stoppingDistance = distanceAttackCastle;
            target = GameController.Instance.robotsCastle.transform;
            Task.current.Succeed();
        }
        else
        {
            distanceSeek = GetComponent<MageScript>().properties.distanceSeek;
            Task.current.Complete(false);
        }
    }
}