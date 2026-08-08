using UnityEngine;

public class BasketController : MonoBehaviour
{
    public float smoothSpeed = 10f;
    public float xLimit = 7f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float targetX = mousePos.x;

            // Clamp inside screen
            targetX = Mathf.Clamp(targetX, -xLimit, xLimit);

            Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);

            // Smooth movement (prevents vibration)
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        }
    }
}