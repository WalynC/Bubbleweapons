using System.Collections.Generic;
using UnityEngine;

public class Gun : Tool
{
    public GameObject bullet;
    public Queue<GameObject> pool = new Queue<GameObject>();

    public override void Use()
    {
        GameObject obj = GetFromPool();
        obj.SetActive(true);
        obj.transform.position = transform.position;
        obj.GetComponent<Bullet>().Init(pool);
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
