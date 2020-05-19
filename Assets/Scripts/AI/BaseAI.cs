using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    public NavMeshAgent agent;

    [Header("Wander")] 
    public float wanderRadius = 10.0f;
    public float wanderDistance = 10.0f;
    //Aumenta a aleatoriedade
    public float jitter = 1.0f;
    private Vector3 wanderTarget = Vector3.zero;
    private int currentWaypoint;


    //Seguir
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

    public void Waypoint() {
        int count;
        do {
            count = Random.Range(0, ManagerWaypoints.Instance.waypoints.Count);
        } while (count == currentWaypoint);
        Vector3 target = ManagerWaypoints.Instance.waypoints[count].position;
        Seek(target);
    }
}
