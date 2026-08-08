using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadReportScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ReportScene");
    }


    public void GoToOptions()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene("OptionsScene");
}
}