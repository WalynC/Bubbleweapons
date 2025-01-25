using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Queue<GameObject> returnPool;
    float timeDie;
    public float timeLimit = 5.0f;

    public void Init(Queue<GameObject> pool)
    {
        returnPool = pool;
        timeDie = Time.time + timeLimit;
    }

    private void OnTriggerEnter(Collider other)
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
