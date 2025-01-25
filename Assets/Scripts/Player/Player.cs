using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance;
    CharacterController characterController;
    public Camera cam;
    public float speed;
    public float jumpHeight;
    public float timeToApex = .5f;
    public float jumpSpeed;
    public float gravity;

    public Vector3 velocity = Vector3.zero;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction attackAction;

    public Tool activeTool;

    public Health health;

    public int bubblePower;
    public int maxBubblePower = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");

        jumpSpeed = 2 * jumpHeight / timeToApex;
        gravity = -2 * jumpHeight / Mathf.Pow(timeToApex, 2);
        bubblePower = maxBubblePower;
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
        else if (jumpAction.IsPressed())
        {
            velocity.y = jumpSpeed;
        }
        else
        {
            velocity.y = 0f;
        }
        characterController.Move(velocity * Time.deltaTime);
        if ((!activeTool.held && attackAction.WasPressedThisFrame())
            || (activeTool.held && attackAction.IsPressed()))
        {
            activeTool?.Use();
        }
    }

    public bool SpendBubblePower(int cost)
    {
        if (cost <= bubblePower)
        {
            bubblePower -= cost;
            return true;
        }
        return false;
    }

    public void Die()
    {
        print("dead");
    }
}
