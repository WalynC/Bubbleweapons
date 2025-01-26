using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public bool held;
    public GameObject visual;
    public abstract void Use();
}
