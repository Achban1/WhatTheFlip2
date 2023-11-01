using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RotationController : MonoBehaviour
{
    public static RotationController Instance { get; private set; }
    public float rotationSpeed = 5f;
    private Quaternion targetRotation;
    private Coroutine flipRoutine;
    public Image redImage;

    CameraScript camerascript;

    void Start()
    {
        Instance = this;
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
        //flipRoutine = StartCoroutine(RandomFlip());
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void StartFlip()
    {
        targetRotation *= Quaternion.Euler(0, 0, 180);
        Invoke(nameof(CameraShakeWithDelay), 0f);
    }

    private void CameraShakeWithDelay()
    {
        camerascript.Shake(0.15f);
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
