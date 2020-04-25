using System;
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
    [SerializeField] private float gatheringSpeed;

    [SerializeField] private GameObject resourceTarget;

    [Header("Animations")] [SerializeField]
    private float distanceStopAnimationPlay = 0.2f;

    private Animator animator;

    private GameObject currentTarget;

    [Header("Actions")] public bool idle = true;
    public bool inMovement;
    public bool gathering;

    public override void InitItems() {
        agent = GetComponent<NavMeshAgent>();
        properties = UnityEngine.Resources.Load<GathererUnit>(nameScriptable);

        life = properties.life;
        speedMovement = properties.speedMovement;
        maxAmountResources = properties.maxAmountResources;
        gatheringSpeed = properties.gatheringSpeed;
    }

    public override void Action(Vector3 target, GameObject targetObject = null) {
        resourceTarget = null;
        print($"Hit: {targetObject.tag} and Vector of Target: {target}");
        if (targetObject.CompareTag("Resources")) {
            MoveTo(target);
            ActiveMovement();
            resourceTarget = targetObject;
        }
        else if (!targetObject.CompareTag("Ground")) return;
        
        MoveTo(target);
        ActiveMovement();
    }

    private void ActiveMovement() {
        idle = false;
        inMovement = true;
        gathering = false;

        // animator.SetBool("inMovement", inMovement);
        // animator.SetBool("Idle", idle);
    }

    private void Update() {
        if (resourceTarget != null) {
            var distance = Vector3.Distance(transform.position, resourceTarget.transform.position);
            if (distance < 1.0f) {
                print($"Distance: {distance}");
                agent.isStopped = true;
            }
        }
        else {
            agent.isStopped = false;
        }
    }
}