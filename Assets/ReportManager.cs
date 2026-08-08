using UnityEngine;
using TMPro;

public class ReportManager : MonoBehaviour
{
    public TextMeshProUGUI finalText;
    public TextMeshProUGUI focusText;
    public TextMeshProUGUI impulseText;
    public TextMeshProUGUI insightText;

    void Start()
    {
        float focus = PlayerPrefs.GetFloat("FocusScore", 0);
        float impulse = PlayerPrefs.GetFloat("ImpulseScore", 0);
        float finalScore = PlayerPrefs.GetFloat("FinalScore", 0);

        // Display values
        focusText.text = "Focus: " + focus.ToString("F1");
        impulseText.text = "Impulse: " + impulse.ToString("F1");
        finalText.text = "Final: " + finalScore.ToString("F1");

        // Generate insight
        insightText.text = GenerateInsight(focus, impulse);
    }

    string GenerateInsight(float focus, float impulse)
    {
        if (focus < 40 && impulse < 40)
        {
            return "Low focus and impulse control. Child may need attention training.";
        }
        else if (focus < 40)
        {
            return "Low focus detected. Difficulty in sustained attention.";
        }
        else if (impulse < 40)
        {
            return "Impulse control is low. Child reacts quickly without stopping.";
        }
        else if (focus > 70 && impulse > 70)
        {
            return "Strong focus and good impulse control.";
        }
        else
        {
            return "Moderate performance. Can improve with practice.";
        }
    }
}