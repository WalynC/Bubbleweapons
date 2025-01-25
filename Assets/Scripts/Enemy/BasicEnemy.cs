using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public float attackRange = 1f;
    EnemyState state = EnemyState.Wander;

    //player spotting variables
    public float visualDistance = 15f;
    public float fov = 90f;
    public float soundDistance = 3f;

    //wander variables
    public float wanderRange = 5f;
    Vector3 home;

    //follow variables
    public float followUpdateDelay = .5f;
    float timeUntilUpdate;
    public float followFailureTime = 3f;
    float timeUntilFollowFail;

    enum EnemyState
    {
        Wander,
        Follow
    }

    private void Start()
    {
        home = transform.position;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, Player.instance.transform.position);
    }

    void Attack()
    {

    }

    bool CanSeePlayer()
    {
        if (DistanceToPlayer() < soundDistance) return true; //if player is too close, they can be heard
        else if (DistanceToPlayer() < visualDistance //sight check
            && Vector3.Angle(transform.forward, Player.instance.transform.position - transform.position) < fov) return true;
        return false;
    }

    public void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.Wander:
                if (agent.remainingDistance < 0.1f)
                {
                    agent.SetDestination(home + new Vector3(Random.Range(-wanderRange, wanderRange), 0, Random.Range(-wanderRange, wanderRange)));
                }
                if (CanSeePlayer())
                {
                    timeUntilFollowFail = Time.time + followFailureTime;
                    state = EnemyState.Follow;
                }
                break;
            case EnemyState.Follow:
                if (DistanceToPlayer() < attackRange)
                {
                    agent.isStopped = true;
                    Attack();
                } else
                {
                    agent.isStopped = false;
                }
                if (Time.time > timeUntilUpdate)
                {
                    agent.SetDestination(Player.instance.transform.position);
                    timeUntilUpdate = Time.time+followUpdateDelay;
                }
                if (CanSeePlayer())
                {
                    timeUntilFollowFail = Time.time + followFailureTime;
                }
                if (Time.time > timeUntilFollowFail) //if we lose track of player, go back to wandering around home
                {
                    agent.isStopped = true;
                    agent.ResetPath();
                    state = EnemyState.Wander;
                }
                break;
        }
    }
}
