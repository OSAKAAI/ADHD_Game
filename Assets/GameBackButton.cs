using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBackButton : MonoBehaviour
{
    public void GoBackToSelection()
    {
        // Resume main menu music
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic();
        }

        // Load Game Selection screen
        SceneManager.LoadScene("GameSelection");
    }
}