using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Vector3Event : UnityEvent<Vector3> {

}

public class GrenadeShot : MonoBehaviour
{
    Queue<GameObject> returnPool;
    float timeDie;
    public float timeLimit = 5.0f;
    public Rigidbody rb;
    public float speed = 10.0f;
    public float verticalVelocity = 0f;

    public Vector3Event dieEvent;

    public void Init(Queue<GameObject> pool, Transform t, Vector3 forward)
    {
        returnPool = pool;
        timeDie = Time.time + timeLimit;
        rb.MovePosition(t.position);
        transform.forward = forward;
        rb.linearVelocity = transform.forward * speed + Vector3.up * verticalVelocity;
        rb.angularVelocity = Vector3.zero;
    }

    void Die()
    {
        dieEvent.Invoke(transform.position);
    }

    public void Return()
    {
        gameObject.SetActive(false);
        returnPool.Enqueue(gameObject);
    }

    private void FixedUpdate()
    {
        if (Time.time > timeDie) { Die(); }
    }
}
