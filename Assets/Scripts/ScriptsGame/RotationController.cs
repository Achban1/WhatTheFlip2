using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RotationController : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private Quaternion targetRotation;
    private Coroutine flipRoutine;
    public Image redImage;

    CameraScript camerascript;

    void Start()
    {
        camerascript = Camera.main.GetComponent<CameraScript>();

        GameObject redImageGO = GameObject.FindGameObjectWithTag("RedImage");
        if (redImageGO != null)
        {
            redImage = redImageGO.GetComponent<Image>();
            if (redImage == null)
            {
                Debug.LogError("Image component not found on RedImage GameObject!");
                return;
            }
        }
        else
        {
            Debug.LogError("RedImage GameObject not found!");
            return;
        }

        // Ensure the red image is initially invisible
        Color color = redImage.color;
        color.a = 0;
        redImage.color = color;

        targetRotation = transform.rotation;
        flipRoutine = StartCoroutine(RandomFlip());
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void StartFlip()
    {
        targetRotation *= Quaternion.Euler(0, 0, 180);
        Invoke(nameof(CameraShakeWithDelay), 0f);
    }

    private void CameraShakeWithDelay()
    {
        camerascript.Shake(0.15f);
    }
    

    private IEnumerator RandomFlip()
    {
        while (true)
        {
            float randomTime = Random.Range(5f, 20f);

            // Wait for the (randomTime - 2) seconds before starting the flash
            yield return new WaitForSeconds(randomTime - 2f);

            // Start flashing and wait for 2 seconds
            StartCoroutine(FlashRedImage());
            yield return new WaitForSeconds(0f); // it was 1.5

            // Then start the flip
            StartFlip();
        }
    }

    private IEnumerator FlashRedImage()
    {
        if (redImage != null)
        {
            for (int i = 0; i < 3; i++) // Flash 3 times
            {
                // Fade in
                while (redImage.color.a < 0.5)
                {
                    Color color = redImage.color;
                    color.a += 10f * Time.deltaTime; // Adjust speed if necessary
                    redImage.color = color;
                    yield return null;
                }

                // Fade out
                while (redImage.color.a > 0)
                {
                    Color color = redImage.color;
                    color.a -= 10f * Time.deltaTime; // Adjust speed if necessary
                    redImage.color = color;
                    yield return null;
                }
            }
        }
    }
}
