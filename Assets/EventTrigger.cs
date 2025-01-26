using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent triggerEvent;

    void OnTriggerEnter(Collider c)
    {
        triggerEvent.Invoke();
    }
}
