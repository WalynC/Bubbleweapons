using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public string mainMenuScene;

    public void Load()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gameObject.SetActive(true);
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
}
