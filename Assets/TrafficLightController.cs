using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public Sprite redLight;
    public Sprite greenLight;

    private SpriteRenderer sr;
    public bool isGreen = true;

    public float minTime = 0.3f; 
    public float maxTime = 1.2f;  

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(SwitchLightRoutine());
    }

    System.Collections.IEnumerator SwitchLightRoutine()
    {
        while (true)
        {
            
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);

            // Switch light
            isGreen = !isGreen;

            if (isGreen)
                sr.sprite = greenLight;
            else
                sr.sprite = redLight;
        }
    }
}