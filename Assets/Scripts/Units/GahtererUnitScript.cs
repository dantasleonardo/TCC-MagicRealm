﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GahtererUnitScript : Robot
{
    [Header("Gatherer Unit Properties")] [SerializeField]
    private string nameScriptable;

    [SerializeField] private GathererUnit properties;
    [SerializeField] private int maxAmountResources;
    [SerializeField] private int currentAmountResources = 0;
    [SerializeField] private float gatheringSpeed;
    [SerializeField] private int amountResources;

    [SerializeField] private Resources resourceTarget;

    [SerializeField] private float turningSpeed = 2.5f;

    private Dictionary<ResourceType, int> inventory = new Dictionary<ResourceType, int>();
    [SerializeField] private bool inventoryIsFull;

    [Header("Animations")] [SerializeField]
    private GameObject countGatherer;

    [SerializeField] private Transform countGathererSpawn;

    public Animator animator;
    private float speed;
    [Header("Actions")] public bool gathering;
    public bool goingToBase;

    [Header("Gatherer Idle")] [SerializeField]
    private GameObject idleIcon;

    [SerializeField] private bool useTime;
    [SerializeField] private float idleTimeCount;
    [SerializeField] private float idleTimeWait = 5.0f;

    #region UnitRelated

    public override void InitItems()
    {
        agent = GetComponent<NavMeshAgent>();

        life = properties.life;
        speedMovement = properties.speedMovement;
        agent.speed = speedMovement;
        maxAmountResources = properties.maxAmountResources;
        gatheringSpeed = properties.gatheringSpeed;
        amountResources = properties.amountResources;
        turningSpeed = properties.turningSpeed;
        robotType = properties.RobotType;
        lifeBar.totalValue = life;
    }

    public override void Action(Vector3 target, GameObject targetObject = null)
    {
        gathering = false;
        if (resourceTarget != null)
            resourceTarget.SetParticles(false);
        StopAllCoroutines();
        resourceTarget = null;
        //print($"Hit: {targetObject.tag} and Vector of Target: {target}");
        if (targetObject != null && targetObject.CompareTag("Resources"))
        {
            resourceTarget = targetObject.GetComponent<Resources>();
            agent.stoppingDistance = 0.5f;
            if (inventoryIsFull)
                GoToBase();
            else
                MoveTo(resourceTarget.transform.position);
        }
        else if (targetObject != null && targetObject.CompareTag("Ground"))
        {
            agent.stoppingDistance = 0.0f;
            MoveTo(target);
        }
    }

    #endregion

    #region GathererUnitRelated

    private IEnumerator GetResource()
    {
        if (resourceTarget != null)
        {
            yield return new WaitForSeconds(gatheringSpeed / 2.5f);

            var countG = Instantiate(countGatherer, countGathererSpawn.position, countGathererSpawn.rotation,
                countGathererSpawn);
            countG.GetComponent<CountGatherer>().SetCountValue(amountResources);
            Debug.Log("Está chamando");

            yield return new WaitForSeconds(gatheringSpeed / 1.5f);

            var resources = resourceTarget.GetResource(amountResources);

            ResourceType key = ResourceType.Stone;
            var value = 0;

            foreach (var resource in resources)
            {
                key = resource.Key;
                value = resource.Value;
            }

            if (inventory.ContainsKey(key))
            {
                inventory[key] += value;
            }
            else
            {
                inventory.Add(key, value);
            }

            print($"Key: {key} Value: {inventory[key]}");
            currentAmountResources += value;


            if (currentAmountResources < maxAmountResources)
            {
                StartCoroutine(GetResource());
            }
            else
            {
                gathering = false;
                inventoryIsFull = true;
                GoToBase();
                goingToBase = true;
            }
        }
    }

    #endregion

    private void Update()
    {
        IdleIconRobot();
        speed = Vector3.Project(agent.desiredVelocity, transform.forward).magnitude;
        animator.SetFloat("Speed", speed);
        if (resourceTarget != null)
        {
            if (goingToBase)
            {
                var distanceOfBase = WithinReach(1f);
                if (!distanceOfBase) return;
                GiveResources();
                return;
            }

            var distance = WithinReach(agent.stoppingDistance);
            if (!distance) return;
            var lookRotation = LookTarget();
            resourceTarget.SetParticles(true);
            if (gathering) return;
            StartCoroutine(GetResource());
            gathering = true;
        }
        else
        {
            gathering = false;
        }
    }

    private void IdleIconRobot()
    {
        if (speed < 0.1f)
        {
            if (useTime)
            {
                idleTimeCount += Time.deltaTime;
                if (idleTimeCount > idleTimeWait)
                {
                    idleIcon.SetActive(true);
                }
            }
            else
            {
                idleIcon.SetActive(true);
            }
        }
        else
        {
            idleIcon.SetActive(false);
            idleTimeCount = 0.0f;
        }
    }

    private bool WithinReach(float distance)
    {
        var isTrue = Vector3.Distance(transform.position, agent.destination) <= distance;
        return isTrue;
    }

    private Quaternion LookTarget()
    {
        Vector3 direction = (resourceTarget.gameObject.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);

        return lookRotation;
    }

    private void GoToBase()
    {
        resourceTarget.SetParticles(false);
        var target = RobotsCastle.Instance.spawnPosition.position;
        MoveTo(target);
    }

    private void GoToResource()
    {
        var target = resourceTarget.gameObject.transform.position;
        MoveTo(target);
    }

    private void GiveResources()
    {
        RobotsCastle.Instance.GetResourcesOfUnit(inventory);
        if (resourceTarget != null)
            GoToResource();
        inventoryIsFull = false;
        goingToBase = false;
        currentAmountResources = 0;
        inventory = new Dictionary<ResourceType, int>();
        foreach (var test in inventory)
        {
            print("Inventario tem alguma coisa");
            print(test.Key + test.Value);
        }
    }
}