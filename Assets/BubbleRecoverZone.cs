using UnityEngine;

public class BubbleRecoverZone : MonoBehaviour
{
    public int refillRate = 1;

    void OnTriggerStay(Collider other)
    {
        Player.instance.bubblePower += refillRate;
        Player.instance.bubblePower = Mathf.Min(Player.instance.bubblePower, Player.instance.maxBubblePower);
    }
}
