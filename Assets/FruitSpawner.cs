using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs;
    public float xRange = 7f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnFruit), 1f, 1f);
    }

    void SpawnFruit()
    {
        int index = Random.Range(0, fruitPrefabs.Length);

        float randomX = Random.Range(-xRange, xRange);
        Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0);

        Instantiate(fruitPrefabs[index], spawnPos, Quaternion.identity);
    }
}