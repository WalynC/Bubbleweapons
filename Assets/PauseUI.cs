using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseUI : MonoBehaviour
{
    public GameObject gameUI;
    public string mainMenuScene;
    InputAction pauseAction;

    void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            Unpause();
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void Restart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void Unpause()
    {
        gameObject.SetActive(false);
        gameUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
}
