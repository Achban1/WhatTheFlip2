using UnityEngine;

public class DevilsFalling : MonoBehaviour
{
    private float time;
    private Vector3 startPosition;
    private float acceleration = 0.1f;
    private float maxSpeed = 6f;
    private float targetTime = 5f;

    private void Start()
    {
        time = 0;
        startPosition = transform.position;
    }

    private void Update()
    {
        if (time < targetTime)
        {
            float currentSpeed = Mathf.Lerp(0, maxSpeed, time / (targetTime / 2));

            if (time <= targetTime / 2)
            {
                transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
                time += Time.deltaTime;
            }
            else
            {
                float linearSpeed = maxSpeed - acceleration * (time - targetTime / 2);
                transform.Translate(Vector3.up * linearSpeed * Time.deltaTime);
                time += Time.deltaTime;
            }
        }
    }
}
