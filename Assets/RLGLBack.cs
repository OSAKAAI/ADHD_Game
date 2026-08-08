using UnityEngine;
using UnityEngine.SceneManagement;

public class RLGLBack : MonoBehaviour
{
    public void GoBack()
    {

        Screen.orientation = ScreenOrientation.Portrait;
        
        Time.timeScale = 1f;

        if (AudioManager.instance != null)
            AudioManager.instance.PlayMusic();

        SceneManager.LoadScene("GameSelection");
    }
}