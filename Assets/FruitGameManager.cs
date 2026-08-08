using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FruitGameManager : MonoBehaviour
{
    public int score = 0;
    public int targetScore = 10;

    int correctCount;
    int wrongCount;

    public float timeElapsed = 0f;
    public bool isGameRunning = true;

    public FruitType currentTarget;

    float targetChangeTimer = 0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI targetText;

    public Image targetIcon;

    public Sprite appleSprite;
    public Sprite grapesSprite;

    public GameObject winPanel;
    public Button exitButton;

    public Button restartButton;

    public TextMeshProUGUI timerText;

    public AudioSource audioSource;

    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip winSound;

    public AudioSource musicSource;

    void Start()
    {
         if (musicSource != null){
        musicSource.Play();
        }

        AudioManager.instance.StopMusic();

        timeElapsed = 0f;    
        isGameRunning = false;   
         winPanel.SetActive(false);
         exitButton.onClick.AddListener(ExitGame);
         restartButton.onClick.AddListener(RestartGame);
        UpdateUI();
    }

    public void CheckFruit(FruitType collectedFruit)
    {
        if (!isGameRunning)
    {
        isGameRunning = true; // start timer here
    }

        if (score >= targetScore) return;

        if (collectedFruit == currentTarget)
        {
            score++;
            audioSource.PlayOneShot(correctSound);
            correctCount++;
        }
        else
        {
            score = Mathf.Max(0, score - 1);
            audioSource.PlayOneShot(wrongSound); 
            wrongCount++; 
        }

        UpdateUI();

        if (score >= targetScore)
        {
            GameWin();
        }
    }

   void PickNewTarget()
{
    FruitType newTarget;

    do
    {
        newTarget = (FruitType)Random.Range(0, 2);
    }
    while (newTarget == currentTarget);

    currentTarget = newTarget;

    if (currentTarget == FruitType.Apple)
        targetIcon.sprite = appleSprite;
    else
        targetIcon.sprite = grapesSprite;

    UpdateUI();
}

void Update()
{
    if (isGameRunning)
    {
        timeElapsed += Time.unscaledDeltaTime;

        timerText.text = "Time: " + timeElapsed.ToString("F1") + "s";
    }

    targetChangeTimer += Time.deltaTime;

if (targetChangeTimer >= 8f)
{
    PickNewTarget();
    targetChangeTimer = 0f;
}
}

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        targetText.text = "Collect: " + currentTarget.ToString();
    }

    void GameWin()
{
    Debug.Log("YOU WIN!");

    // STOP GAME
    isGameRunning = false;
    Time.timeScale = 0f;   

    // SHOW WIN UI
    winPanel.SetActive(true);

    // STOP TARGET CHANGE / SPAWN
    CancelInvoke(nameof(PickNewTarget));

    // CALCULATE SCORES
    float accuracy = (float)correctCount / (correctCount + wrongCount);
    float focusScore = accuracy * 100f;

    float rlglReaction = PlayerPrefs.GetFloat("RLGL_ReactionTime", 0f);
    float rlglTime = PlayerPrefs.GetFloat("RLGL_LevelTime", 0f);

    float reactionScore = Mathf.Clamp(100f - (rlglReaction * 20f), 0f, 100f);
    float timeScore = Mathf.Clamp(100f - (rlglTime * 2f), 0f, 100f);

    float impulseScore = (reactionScore + timeScore) / 2f;
    float finalScore = (focusScore + impulseScore) / 2f;

    // SAVE DATA
    PlayerData data = new PlayerData();

    data.playerName = "Child4";
    data.fruitCorrect = correctCount;
    data.fruitWrong = wrongCount;
    data.fruitReactionTime = timeElapsed;

    data.rlglReactionTime = rlglReaction;

    data.focusScore = focusScore;
    data.impulseScore = impulseScore;
    data.finalScore = finalScore;

    PlayerPrefs.SetFloat("FocusScore", focusScore);
    PlayerPrefs.SetFloat("ImpulseScore", impulseScore);
    PlayerPrefs.SetFloat("FinalScore", finalScore);

    PlayerPrefs.Save(); 

    FirebaseManager.instance.SaveData(data);

    Debug.Log("Final Score: " + finalScore);
}

public void RestartGame()
{
    Debug.Log("Restarting Game");

    // Resume time
    Time.timeScale = 1f;

    // Reset score
    score = 0;

    correctCount = 0;
    wrongCount = 0;

    // Reset timer
    timeElapsed = 0f;
    isGameRunning = true;

    winPanel.SetActive(false);

    if (musicSource != null)
        musicSource.Play();

    PickNewTarget();
    UpdateUI();
}

public void ExitGame()
{
    Debug.Log("Exit Game");

    Application.Quit();
}

}