using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject gameUI;
    public string mainMenuScene;

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
