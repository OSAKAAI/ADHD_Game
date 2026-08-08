using UnityEngine;

public class FruitFall : MonoBehaviour
{
    public float fallSpeed = 5f;
    public FruitType fruitType;

    FruitGameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<FruitGameManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Basket"))
        {
            gameManager.CheckFruit(fruitType);
            Destroy(gameObject);
        }
    }
}