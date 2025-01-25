using UnityEngine;

public class HealthGauge : MonoBehaviour
{
    RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }
    void Update()
    {
        rt.anchorMax = new Vector2((float)Player.instance.health.health / (float)Player.instance.health.maxHealth, rt.anchorMax.y);
    }
}
