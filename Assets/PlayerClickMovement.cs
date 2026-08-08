using UnityEngine;

public class PlayerClickMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private bool isMoving = false;

    private Animator animator; 
    public TrafficLightController trafficLight;

    // Safe time system
    float redStartTime = 0f;
    float safeTime = 1f;
    bool wasGreenLastFrame = true;
    bool canCheckMovement = false;

    // ADHD tracking
    float maxReactionTime = 0f;
    bool reactionCaptured = false;
    bool wasMovingAtRed = false;

    float levelTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        maxReactionTime = 0f;
    }

    void Update()
    {
        // ⏱ Track total level time
        levelTime += Time.deltaTime;

        // 🖱 Input
        if (Input.GetMouseButtonDown(0))
            isMoving = true;

        if (Input.GetMouseButtonUp(0))
            isMoving = false;

        // Detect Green → Red
        if (wasGreenLastFrame && !trafficLight.isGreen)
        {
            redStartTime = Time.time;
            canCheckMovement = false;
            reactionCaptured = false;

            
            wasMovingAtRed = isMoving;
        }

        wasGreenLastFrame = trafficLight.isGreen;

        // ⏱ Allow checking after safe time
        if (!trafficLight.isGreen && !canCheckMovement)
        {
            if (Time.time - redStartTime >= safeTime)
                canCheckMovement = true;
        }

        // Capture reaction when player stops
        if (!trafficLight.isGreen && !reactionCaptured && wasMovingAtRed && !isMoving)
        {
            float currentReaction = Time.time - redStartTime;

            if (currentReaction > 0.05f && currentReaction > maxReactionTime)
                maxReactionTime = currentReaction;

            reactionCaptured = true;

            Debug.Log("Reaction Time: " + currentReaction);
        }

        // Game over condition
        if (isMoving && !trafficLight.isGreen && canCheckMovement)
        {
            if (wasMovingAtRed)
            {
                float currentReaction = Time.time - redStartTime;

                if (currentReaction > maxReactionTime)
                    maxReactionTime = currentReaction;

                Debug.Log("Reaction (fail): " + currentReaction);
            }

            SaveData();
            Die();
        }

        animator.SetBool("isRunning", isMoving);
    }

    void FixedUpdate()
    {
        if (isMoving && trafficLight.isGreen)
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    void SaveData()
    {
        if (maxReactionTime <= 0f)
        {
            Debug.LogWarning("Max reaction is zero, not saving!");
            return;
        }

        Debug.Log("Saving Max Reaction: " + maxReactionTime);

        PlayerPrefs.SetFloat("RLGL_ReactionTime", maxReactionTime);
        PlayerPrefs.SetFloat("RLGL_LevelTime", levelTime);
    }

    void Die()
    {
        Debug.Log("GAME OVER");

        isMoving = false;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isRunning", false);

        Time.timeScale = 0f;

        GameManager.instance.GameOver();
    }

    void OnDisable()
{
    SaveData();
}

}

