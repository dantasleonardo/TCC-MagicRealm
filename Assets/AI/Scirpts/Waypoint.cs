using System.Linq;
using UnityEngine;

public class Waypoint : StateMachineBehaviour {
    private BotAi bot;

    public float timeNextTarget = 2.0f;
    public float timeCount = 0.0f;
    public int strikeAmount;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        bot = animator.GetComponent<BotAi>();
        bot.baseAi.Waypoint();
        bot.baseAi.agent.stoppingDistance = 0.0f;
        bot.distanceSeek = bot.gameObject.GetComponent<MageScript>().properties.distanceSeek;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (bot.baseAi.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && bot.baseAi.agent.remainingDistance == 0) {
            timeCount += Time.deltaTime;
            if (timeCount > timeNextTarget) {
                bot.baseAi.Waypoint();
                timeCount = 0.0f;
            }
        }

        if (GameController.Instance.resources[ResourceType.Stone] > strikeAmount || GameController.Instance.resources[ResourceType.Wood] > strikeAmount) {
            bot.distanceSeek = 100;
            bot.target = GameController.Instance.robotsCastle.spawnPosition;
            bot.UpdateAnimation(true, false, false, false);
        }
        else {
            bot.distanceSeek = bot.gameObject.GetComponent<MageScript>().properties.distanceSeek;
            UnitController.Instance.units.ForEach(u => {
                if (bot.target == null) {
                    if (Vector3.Distance(bot.transform.position, u.transform.position) <= bot.distanceSeek) {
                        bot.target = u.transform;
                        bot.UpdateAnimation(true, false, false, false);
                    }
                }
            });
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
