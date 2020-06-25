using UnityEngine;

public class Attacking : StateMachineBehaviour
{
    private BotAi bot;

    public float timeFirerate = 2.0f;
    public float timeCount = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bot = animator.GetComponent<BotAi>();
        bot.baseAi.Waypoint();
        timeCount = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bot.target != null)
        {
            if (Vector3.Distance(bot.transform.position, bot.target.position) > bot.baseAi.agent.stoppingDistance)
                bot.UpdateAnimation(true, false, false, false);
            timeCount += Time.deltaTime;
            bot.LookTarget();
            if (timeCount > timeFirerate)
            {
                bot.enemy.Attack(0);
                timeCount = 0.0f;
            }
        }
        else
        {
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