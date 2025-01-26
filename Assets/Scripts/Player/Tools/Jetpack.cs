using UnityEngine;

public class Jetpack : Tool
{
    public Player player;
    public ParticleSystem ps;
    float timeUntilStopPlay = 0f;
    public override void Use()
    {
        if (player.SpendBubblePower(1))
        {
            player.velocity.y = Mathf.Min(player.velocity.y - (player.gravity * 2 * Time.deltaTime), player.jumpSpeed);
            if (!ps.isPlaying) ps.Play();
            timeUntilStopPlay = Time.time + 1f;
        }
    }

    private void Update()
    {
        if (ps.isPlaying && Time.time > timeUntilStopPlay) ps.Stop();
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
