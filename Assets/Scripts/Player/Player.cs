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
    bool attackPending = false;
    InputAction nextAction;
    InputAction previousAction;

    public Health health;

    public int bubblePower;
    public int maxBubblePower = 100;

    public Transform toolContainer;
    Tool[] toolArray;
    int currentTool = 0;
    public Tool activeTool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        attackAction = InputSystem.actions.FindAction("Attack");
        nextAction = InputSystem.actions.FindAction("Next");
        previousAction = InputSystem.actions.FindAction("Previous");

        jumpSpeed = 2 * jumpHeight / timeToApex;
        gravity = -2 * jumpHeight / Mathf.Pow(timeToApex, 2);
        bubblePower = maxBubblePower;

        toolArray = new Tool[toolContainer.childCount];
        for (int i = 0; i < toolArray.Length; i++)
        {
            toolArray[i] = toolContainer.GetChild(i).GetComponent<Tool>();
            if (toolArray[i] == activeTool)
            {
                activeTool.visual.SetActive(true);
                currentTool = i;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;
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
        if (attackPending)
        {
            activeTool.Use();
        }
        attackPending = false;
    }
    private void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;
        int change = 0;
        if (nextAction.WasPressedThisFrame())
        {
            change++;
        }
        if (previousAction.WasPressedThisFrame())
        {
            change--;
        }
        if (change != 0)
        {
            currentTool += change;
            currentTool += toolArray.Length;
            currentTool %= toolArray.Length;
            activeTool.visual.SetActive(false);
            activeTool = toolArray[currentTool];
            activeTool.visual.SetActive(true);
        }
        if ((!activeTool.held && attackAction.WasPressedThisFrame())
            || (activeTool.held && attackAction.IsPressed()))
        {
            attackPending = true;
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
