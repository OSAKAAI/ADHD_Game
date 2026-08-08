using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject levelCompletePanel;
    public GameObject gameOverPanel;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Set landscape when game starts
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        Time.timeScale = 1f;

        // Stop menu music
        if (AudioManager.instance != null)
            AudioManager.instance.StopMusic();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;   // pause game
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelComplete()
    {
        Time.timeScale = 0f;
        levelCompletePanel.SetActive(true);
    }

    // Back to GameSelection
    public void ExitToMenu()
    {
        Time.timeScale = 1f;

        // Switch back to portrait
        Screen.orientation = ScreenOrientation.Portrait;

        // Resume menu music
        if (AudioManager.instance != null)
            AudioManager.instance.PlayMusic();

        SceneManager.LoadScene("GameSelection");
    }
}