using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float timeElapsed = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        timeElapsed += Time.deltaTime;
        timerText.text = "Time: " + timeElapsed.ToString("F2");
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        timeElapsed = 0f;
        isRunning = true;
    }
}