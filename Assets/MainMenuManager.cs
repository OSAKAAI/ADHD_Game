using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OpenPlay()
    {
        SceneManager.LoadScene("GameSelection");
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }
}