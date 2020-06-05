//using Panda;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    public NavMeshAgent agent;
    BotAi ai;

    [Header("Wander")] 
    public float wanderRadius = 10.0f;
    public float wanderDistance = 10.0f;
    //Aumenta a aleatoriedade
    public float jitter = 1.0f;
    private Vector3 wanderTarget = Vector3.zero;
    private int currentWaypoint;

    private void Start() {
        ai = GetComponent<BotAi>();
    }


    //Seguir
   //[Task]
    public void Seek(Vector3 target)
    {
        agent.SetDestination(target);
    }

    //Fugir no sentido oposto
    public void Flee(Vector3 target)
    {
        //Calcular a direção oposta do target
        Vector3 direction = transform.position - target;
        Seek(transform.position + target);
    }

    public void Wander()
    {
        wanderTarget += new Vector3(
            Random.Range(-1.0f, 1.0f) * jitter,
            0.0f,
            Random.Range(-1.0f, 1.0f) * jitter
        );
        wanderTarget = wanderTarget.normalized * wanderRadius;
        //wanderTarget = this.wanderTarget.normalized * this.wanderTarget;
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = transform.InverseTransformVector(targetLocal);
        Seek(targetWorld);
    }

   // [Task]
    public void Waypoint() {
        int count;
        do {
            count = Random.Range(0, ManagerWaypoints.Instance.waypoints.Count);
        } while (count == currentWaypoint);
        Vector3 target = ManagerWaypoints.Instance.waypoints[count].position;
        Seek(target);
       // Task.current.Succeed();
    }

   // [Task]
    public void AgentArrivedDestination() {
        if(Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance) {
            //ask.current.Succeed();
        }
    }

    //[Task]
    public void GetEnemyDistance() {
        Debug.Log("GetEnemyDistance");
        if (ai.target == null) {
            ai.distanceSeek = ai.gameObject.GetComponent<MageScript>().properties.distanceSeek;
            UnitController.Instance.units.ForEach(u => {
                Debug.Log($"Target {u.name}, Distance {Vector3.Distance(ai.transform.position, u.transform.position)}");
                if (Vector3.Distance(ai.transform.position, u.transform.position) <= ai.distanceSeek) {
                    ai.target = u.transform;
                    Seek(ai.target.position);
                    //Task.current.Succeed();
                }
            });
        }
    }

    //[Task]
    public void AttackTarget() {
        if (ai.target != null) {
            if (Vector3.Distance(ai.transform.position, ai.target.position) > agent.stoppingDistance)
                ai.timeCount += Time.deltaTime;
            ai.LookTarget();
            if (ai.timeCount > ai.timeFirerate) {
                ai.enemy.Attack(0);
                ai.timeCount = 0.0f;
            }
        }
    }

    /*
     while
			HaveTarget
			SeekTarget()
    */

    //[Task]
    bool HaveTarget() {
        if (ai.target != null)
            return true;
        else
            return false;
    }

    //[Task]
    void SeekTarget() {
        var target = ai.target;
        if(target != null) {
            if (Vector3.Distance(ai.transform.position, ai.target.position) > ai.distanceSeek) {
                ai.target = null;
                //Task.current.Succeed();
            }
            else if(Vector3.Distance(ai.transform.position, ai.target.position) > agent.stoppingDistance) {
                Seek(target.position);
                //Task.current.Succeed();
            }
        }
    }
}
