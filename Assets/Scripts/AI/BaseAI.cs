using UnityEngine;
using UnityEngine.AI;

public class BaseAI : MonoBehaviour
{
    private NavMeshAgent agent;


    private void Awake()
    {
        agent.GetComponent<NavMeshAgent>();
    }


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

    }
}
