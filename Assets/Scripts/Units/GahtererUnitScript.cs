﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Debug = System.Diagnostics.Debug;

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
    private float distanceStopAnimationPlay = 0.2f;

    private Animator animator;

    [Header("Actions")] public bool idle = true;
    public bool inMovement;
    public bool gathering;
    public bool goingToBase;

    #region UnitRelated

    public override void InitItems() {
        agent = GetComponent<NavMeshAgent>();
        properties = UnityEngine.Resources.Load<GathererUnit>(nameScriptable);

        life = properties.life;
        speedMovement = properties.speedMovement;
        agent.speed = speedMovement;
        maxAmountResources = properties.maxAmountResources;
        gatheringSpeed = properties.gatheringSpeed;
        amountResources = properties.amountResources;
        turningSpeed = properties.turningSpeed;
    }

    public override void Action(Vector3 target, GameObject targetObject = null) {
        StopAllCoroutines();
        resourceTarget = null;
        print($"Hit: {targetObject.tag} and Vector of Target: {target}");
        if (targetObject.CompareTag("Resources")) {
            agent.stoppingDistance = 1.0f;
            MoveTo(target);
            ActiveMovement();
            resourceTarget = targetObject.GetComponent<Resources>();
        }
        else if (!targetObject.CompareTag("Ground")) return;

        agent.stoppingDistance = 0.0f;
        MoveTo(target);
        ActiveMovement();
    }

    private void ActiveMovement() {
        idle = false;
        inMovement = true;
        gathering = false;
        goingToBase = false;

        // animator.SetBool("inMovement", inMovement);
        // animator.SetBool("Idle", idle);
    }

    #endregion

    #region GathererUnitRelated

    private IEnumerator GetResource() {
        yield return new WaitForSeconds(gatheringSpeed);

        var resources = resourceTarget.GetResource(amountResources);

        ResourceType key = ResourceType.Rock;
        var value = 0;

        foreach (var resource in resources) {
            key = resource.Key;
            value = resource.Value;
        }

        if (inventory.ContainsKey(key)) {
            inventory[key] += value;
        }
        else {
            inventory.Add(key, value);
        }

        print($"Key: {key} Value: {inventory[key]}");
        currentAmountResources += value;

        if (currentAmountResources < maxAmountResources) {
            StartCoroutine(GetResource());
        }
        else {
            gathering = false;
            inventoryIsFull = true;
            GoToBase();
            goingToBase = true;
        }
    }

    #endregion

    private void Update() {
        if (resourceTarget != null) {
            if (goingToBase) {
                var distanceOfBase = WithinReach(1f);
                if (!distanceOfBase) return;
                GiveResources();
                return;
            }
            var distance = WithinReach(agent.stoppingDistance);
            if (!distance) return;
            var lookRotation = LookTarget();
            if (!(Vector3.Magnitude(lookRotation.eulerAngles - transform.rotation.eulerAngles) < 2.5f)) return;
            if (gathering || inventoryIsFull) return;
            StartCoroutine(GetResource());
            gathering = true;
        }
        else {
            // agent.isStopped = false;
        }
    }

    private bool WithinReach(float distance) {
        var isTrue = Vector3.Distance(transform.position, agent.destination) <= distance;
        return isTrue;
    }

    private Quaternion LookTarget() {
        Vector3 direction = (resourceTarget.gameObject.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);

        return lookRotation;
    }

    private void GoToBase() {
        var target = MainBase.Instance.gameObject.transform.position;
        MoveTo(target);
    }

    private void GoToResource() {
        var target = resourceTarget.gameObject.transform.position;
        MoveTo(target);
    }

    private void GiveResources() {
        MainBase.Instance.GetResourcesOfUnit(inventory);
        GoToResource();
        ActiveMovement();
        inventoryIsFull = false;
        currentAmountResources = 0;
        inventory = new Dictionary<ResourceType, int>();
        foreach (var test in inventory) {
            print("Inventario tem alguma coisa");
            print(test.Key + test.Value);
        }
    }
}