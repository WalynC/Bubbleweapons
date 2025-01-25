using TMPro;
using UnityEngine;

public class ActiveToolUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Update()
    {
        text.text = Player.instance.activeTool.name;
    }
}
