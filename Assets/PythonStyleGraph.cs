using UnityEngine;
using TMPro;
using System.Collections;

public class PythonStyleGraph : MonoBehaviour
{
    [Header("Bars")]
    public RectTransform focusBar;
    public RectTransform impulseBar;
    public RectTransform finalBar;

    [Header("Value Texts")]
    public TextMeshProUGUI focusText;
    public TextMeshProUGUI impulseText;
    public TextMeshProUGUI finalText;

    [Header("Graph Settings")]
    public float maxHeight = 300f;   // Max height of bars
    public float maxValue = 50f;     // Max expected value

    [Header("Animation")]
    public float animationDuration = 0.5f;

    // Call this when data is ready
    public void UpdateGraph(float focus, float impulse, float finalScore)
    {
        // auto-scale 
        maxValue = Mathf.Max(focus, impulse, finalScore, 10f);

        float focusHeight = (focus / maxValue) * maxHeight;
        float impulseHeight = (impulse / maxValue) * maxHeight;
        float finalHeight = (finalScore / maxValue) * maxHeight;

        // Animate bars
        StartCoroutine(AnimateBar(focusBar, focusHeight));
        StartCoroutine(AnimateBar(impulseBar, impulseHeight));
        StartCoroutine(AnimateBar(finalBar, finalHeight));

        // Update text values
        focusText.text = focus.ToString();
        impulseText.text = impulse.ToString();
        finalText.text = finalScore.ToString();
    }

    // Smooth animation coroutine
    IEnumerator AnimateBar(RectTransform bar, float targetHeight)
    {
        float time = 0f;
        float startHeight = bar.sizeDelta.y;

        while (time < animationDuration)
        {
            time += Time.deltaTime;
            float height = Mathf.Lerp(startHeight, targetHeight, time / animationDuration);
            bar.sizeDelta = new Vector2(bar.sizeDelta.x, height);
            yield return null;
        }

        // Ensure final value is exact
        bar.sizeDelta = new Vector2(bar.sizeDelta.x, targetHeight);
    }

    void Start()
{
    float focus = PlayerPrefs.GetFloat("FocusScore", 0f);
    float impulse = PlayerPrefs.GetFloat("ImpulseScore", 0f);
    float finalScore = PlayerPrefs.GetFloat("FinalScore", 0f);

    Debug.Log("Graph Data → Focus: " + focus + " Impulse: " + impulse + " Final: " + finalScore);

    UpdateGraph(focus, impulse, finalScore);
}
}