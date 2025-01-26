using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    InputAction lookAction;
    float vertRot = 0;
    float horiRot = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        horiRot = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;
        Vector2 look = lookAction.ReadValue<Vector2>();
        horiRot += look.x;
        vertRot -= look.y;
        vertRot = Mathf.Clamp(vertRot, -90f, 90f);
        transform.rotation = Quaternion.Euler(vertRot, horiRot,0);
    }
}
