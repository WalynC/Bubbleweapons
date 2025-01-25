using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public float wanderRange = 5f;
    Vector3 home;
    public NavMeshAgent agent;

    private void Start()
    {
        home = transform.position;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (agent.remainingDistance < 0.1f)
        {
            agent.SetDestination(home+new Vector3(Random.Range(-wanderRange,wanderRange), 0, Random.Range(-wanderRange, wanderRange)));
        }
    }
}
