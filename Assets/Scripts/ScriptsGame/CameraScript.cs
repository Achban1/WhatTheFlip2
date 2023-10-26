using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform cameraTransform;
    public float shakeMagnitude = 0.2f;
    private Vector3 originalPosition;

    void Start()
    {
        cameraTransform = transform;
        originalPosition = cameraTransform.localPosition;
    }

    public void Shake(float shakeDuration)
    {
        originalPosition = cameraTransform.localPosition; // Update original position before shaking
        InvokeRepeating("StartShaking", 0f, 0.02f); // Adjusted repeat rate
        Invoke("StopShaking", shakeDuration);
    }

    private void StartShaking()
    {
        float xOffset = Random.Range(-1f, 1f) * shakeMagnitude;
        float yOffset = Random.Range(-1f, 1f) * shakeMagnitude;

        Vector3 pos = originalPosition + new Vector3(xOffset, yOffset, 0f);
        cameraTransform.localPosition = pos;
    }

    private void StopShaking()
    {
        CancelInvoke("StartShaking");
        cameraTransform.localPosition = originalPosition;
    }
}
