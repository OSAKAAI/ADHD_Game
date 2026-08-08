using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelector : MonoBehaviour
{
    public void LoadFruitGame()
    {
        SceneManager.LoadScene("Fruit_Game");
    }

    public void LoadGreenLightGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Green_Light_Game");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
