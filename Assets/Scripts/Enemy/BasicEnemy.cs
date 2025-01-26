using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public NavMeshAgent agent;
    EnemyState state = EnemyState.Wander;

    //attacking
    public float attackRange = 1f;
    public int damage = 5;
    float timeUntilNextAttack;
    public float attackDelay = 1f;

    //player spotting
    public float visualDistance = 15f;
    public float fov = 90f;
    public float soundDistance = 3f;
    public LayerMask sightMask;

    //wander
    public float wanderRange = 5f;
    Vector3 home;

    //follow
    public float followUpdateDelay = .5f;
    float timeUntilUpdate;
    public float followFailureTime = 3f;
    float timeUntilFollowFail;

    public AudioSource foundSound;

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
        if (Time.time > timeUntilNextAttack)
        {
            Player.instance.health.TakeDamage(damage);
            timeUntilNextAttack = Time.time + attackDelay;
        }
    }

    bool CanSeePlayer()
    {
        if (DistanceToPlayer() < soundDistance) return true; //if player is too close, they can be heard
        Vector3 dir = Player.instance.transform.position - transform.position;
        if (DistanceToPlayer() < visualDistance && Vector3.Angle(transform.forward, dir) < fov) //sight check
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, visualDistance, sightMask))
            {
                if (hit.transform == Player.instance.transform) return true;
            }
        }
        return false;
    }

    public void FixedUpdate()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;
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
                    foundSound.Play();
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
