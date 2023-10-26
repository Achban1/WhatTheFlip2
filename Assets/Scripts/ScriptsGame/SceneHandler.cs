using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public float moveSpeed = 3f;
    public static float lerpDuration = 0.2f;
    private Vector3 startPosition;
    private Vector3 centerPosition;
    private Vector3 endPosition;
    private float timer;
    private bool changingScene = false;
    private bool openingThisScene = true;

    public ScoreManager scoreManager;

    private void Start()
    {
        timer = 0f;
        startPosition = new Vector3(0, 14, 0);
        centerPosition = new Vector3(0, 0, 0);
        endPosition = new Vector3(0, -14, 0);

        transform.position = startPosition;
    }

    private void Update()
    {
        SceneOpening();
        ChangeScene();
        if (Input.GetKeyDown(KeyCode.N))
        {
            changingScene = true;
            timer = 0f;
            Invoke(nameof(LoadNextScene), lerpDuration);
            //PlayerMovement.Instance.DisableAllMovement();
        }
        
    }
    private void SceneOpening()
    {
        if (openingThisScene)
        {
            float t = timer / lerpDuration;
            float easedT = EaseInAndOut(0f, 1f, t*2);
            transform.position = Vector3.Lerp(startPosition, centerPosition, easedT);
            timer += Time.deltaTime;
            if (transform.position == centerPosition)
            {
                openingThisScene = false;
            }
        }
    }

    public void ChangeScene()
    {
        if (changingScene)
        {
            float t = timer / lerpDuration;
            float easedT = EaseInAndOut(0f, 1f, t*2);
            transform.position = Vector3.Lerp(centerPosition, endPosition, easedT);
            timer += Time.deltaTime;
        }
    }

    private float EaseInAndOut(float edge0, float edge1, float x)
    {
        x = Mathf.Clamp01((x - edge0) / (edge1 - edge0)); // Clamping x to the range [0, 1]
        return x * x *(3 - 2 * x);
    }

    public void LoadNextScene()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene;

        do
        {
            nextScene = Random.Range(2, sceneCount);
        } while (nextScene == currentSceneIndex);

        SceneManager.LoadScene(nextScene);
        changingScene = false;
    }

    public void SceneCallDeathArea()
    {
        if (scoreManager.player1Score < scoreManager.winningScore && scoreManager.player2Score < scoreManager.winningScore)
        {
            changingScene = true;
            timer = 0f;
            Invoke(nameof(LoadNextScene), lerpDuration);
        }
    }
}
