using System;

[Serializable]
public class PlayerData
{
    // Info
    public string playerName;
    public string sessionId;   
    public string dateTime;

    // Fruit Game (Focus)
    public int fruitCorrect;
    public int fruitWrong;
    public float fruitReactionTime;

    // Red Light Green Light (Impulse Control)
    public float rlglReactionTime;

    // Final Scores
    public float focusScore;
    public float impulseScore;
    public float finalScore;
}