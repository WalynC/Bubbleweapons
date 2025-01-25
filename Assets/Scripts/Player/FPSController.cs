using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    CharacterController characterController;
    public Camera cam;
    public float speed;
    public float jumpHeight;
    public float timeToApex = .5f;
    float jumpSpeed;
    float gravity;

    Vector3 velocity = Vector3.zero;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction attackAction;

    public Tool activeTool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");

        jumpSpeed = 2 * jumpHeight / timeToApex;
        gravity = -2 * jumpHeight / Mathf.Pow(timeToApex, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 inputDir = moveAction.ReadValue<Vector2>();
        Vector3 right = cam.transform.TransformDirection(Vector3.right);
        Vector3 forward = Vector3.Cross(right, Vector3.up);
        velocity = new Vector3(0, velocity.y, 0) + (right * inputDir.x * speed) + (forward * inputDir.y * speed);
        if (!characterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (jumpAction.ReadValue<float>() > .5f)
        {
            velocity.y = jumpSpeed;
        }
        else
        {
            velocity.y = 0f;
        }
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Update()
    {
        if (attackAction.WasPressedThisFrame())
        {
            activeTool?.Use();
        }
    }
}
