using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayUI : MonoBehaviour
{
    InputAction pauseAction;
    public GameObject pause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            gameObject.SetActive(false);
            pause.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }
}
