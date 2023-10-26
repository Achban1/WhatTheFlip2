using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathArea : MonoBehaviour
{
    private SceneHandler sceneHandler;
    public float sceneChangePaus = 0.5f;

    private void Start()
    {
        sceneHandler = FindObjectOfType<SceneHandler>();
        if (sceneHandler == null)
        {
            Debug.LogError("No SceneHandler found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            // Identify which player was killed
            int killedPlayerNumber = collision.CompareTag("Player1") ? 1 : 2;

            // Increment the score of the other player
            ScoreManager.Instance.IncrementScore(killedPlayerNumber == 1 ? 2 : 1);

            // Reload the scene after a small delay
            StartCoroutine(ReloadScene());
        }

        else
        {
            return;
        }
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(sceneChangePaus); // Wait for 3 seconds (adjust as needed)

        if (sceneHandler != null)
        {
            sceneHandler.SceneCallDeathArea(); // Call the SceneCallDeatArea method from the SceneHandler script
        }
    }
}
