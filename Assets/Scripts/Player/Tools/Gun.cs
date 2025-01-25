using System.Collections.Generic;
using UnityEngine;

public class Gun : Tool
{
    public GameObject bullet;
    public Queue<GameObject> pool = new Queue<GameObject>();
    public Player player;

    public override void Use()
    {
        GameObject obj = GetFromPool();
        obj.SetActive(true);
        obj.GetComponent<Bullet>().Init(pool, transform, player.cam.transform.forward);
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
