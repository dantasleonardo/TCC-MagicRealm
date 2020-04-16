using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Unit
{

    #region FunctionsOfUnit

    public override void Action(Vector3 target, GameObject targetObject) {
        if (targetObject.CompareTag("Ground")) {
            MoveTo(target);
        }
    }

    #endregion
    
    
    private void MoveTo(Vector3 target) {
        agent.SetDestination(target);
    }
}
