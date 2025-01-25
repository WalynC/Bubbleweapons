using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public UnityEvent hitEvent;
    public UnityEvent dieEvent;

    void Start()
    {
        health = maxHealth;   
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        hitEvent?.Invoke();
        if (health <= 0)
        {
            dieEvent?.Invoke();
        }
    }
}
