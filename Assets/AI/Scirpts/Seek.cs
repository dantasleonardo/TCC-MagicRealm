using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : StateMachineBehaviour {
    private BotAi bot;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        bot = animator.GetComponent<BotAi>();
        bot.baseAi.Seek(bot.target.position);
        bot.baseAi.agent.stoppingDistance = bot.distanceAttack;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (bot.target != null) {
            if (Vector3.Distance(bot.transform.position, bot.target.position) > bot.distanceSeek) {
                bot.UpdateAnimation(false, true, false, false);
                bot.target = null;
            }
            else {
                if (bot.currentSpeed < 0.4f)
                    bot.UpdateAnimation(false, false, false, true);
                else
                    bot.baseAi.Seek(bot.target.position);
            }
        }
        else {
            bot.UpdateAnimation(false, true, false, false);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
