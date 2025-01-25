using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Queue<GameObject> returnPool;
    float timeDie;
    public float timeLimit = 5.0f;
    public Rigidbody rb;
    public float speed = 10.0f;

    public void Init(Queue<GameObject> pool, Transform t, Vector3 forward)
    {
        returnPool = pool;
        timeDie = Time.time + timeLimit;
        rb.MovePosition(t.position);
        transform.forward = forward;
        rb.linearVelocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Return();
    }

    void Return()
    {
        gameObject.SetActive(false);
        returnPool.Enqueue(gameObject);
    }

    private void FixedUpdate()
    {
        if (Time.time > timeDie) { Return(); }
    }
}
