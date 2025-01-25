using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public bool held;
    public abstract void Use();
}
