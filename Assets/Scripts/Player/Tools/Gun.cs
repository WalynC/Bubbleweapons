using System.Collections.Generic;
using UnityEngine;

public class Gun : Tool
{
    public GameObject bullet;
    public Queue<GameObject> pool = new Queue<GameObject>();

    public override void Use()
    {
        GameObject obj = GetFromPool();
        obj.transform.position = transform.position;
    }

    GameObject GetFromPool()
    {
        if (pool.Count == 0)
        {
            pool.Enqueue(Instantiate(bullet));
        }
        return pool.Dequeue();
    }
}
