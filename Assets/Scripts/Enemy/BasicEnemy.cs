using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
