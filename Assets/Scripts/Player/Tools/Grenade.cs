using System.Collections.Generic;
using UnityEngine;

public class Grenade : Tool
{
    public GameObject bullet;
    public Queue<GameObject> pool = new Queue<GameObject>();
    public Player player;

    public override void Use()
    {
        if (player.SpendBubblePower(15))
        {
            GameObject obj = GetFromPool();
            obj.SetActive(true);
            obj.GetComponent<GrenadeShot>().Init(pool, transform, player.cam.transform.forward);
        }
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
