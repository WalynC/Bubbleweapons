using UnityEngine;
using UnityEngine.Events;

public class WinTrigger : MonoBehaviour
{
    public UnityEvent triggerEvent;

    void OnTriggerEnter(Collider c)
    {
        triggerEvent.Invoke();
    }
}
