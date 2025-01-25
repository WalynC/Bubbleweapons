using UnityEngine;

public class BubbleGauge : MonoBehaviour
{
    RectTransform rt;

    private void Start()
    {
        rt = GetComponent<RectTransform>();
    }
    void Update()
    {
        rt.anchorMax = new Vector2((float)Player.instance.bubblePower/(float)Player.instance.maxBubblePower, rt.anchorMax.y);
    }
}
