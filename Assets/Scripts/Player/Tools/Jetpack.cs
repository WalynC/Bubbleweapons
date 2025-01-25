using UnityEngine;

public class Jetpack : Tool
{
    public Player player;
    public override void Use()
    {
        if (player.SpendBubblePower(1))
        {
            player.velocity.y = Mathf.Min(player.velocity.y - (player.gravity * 2 * Time.deltaTime), player.jumpSpeed);
        }
    }
}
