using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int player1Score = 0;
    public int player2Score = 0;
    public int winningScore = 10; // You can set this value in the Unity Editor

    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    public GameObject WinnerPlayer1;
    public GameObject WinnerPlayer2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        WinnerPlayer1.SetActive(false);
        WinnerPlayer2.SetActive(false);
    }

    public void IncrementScore(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1Score++;
            if (player1ScoreText) player1ScoreText.text = "Player 1: " + player1Score.ToString();
            CheckPlayer1Win();
        }
        else if (playerNumber == 2)
        {
            player2Score++;
            if (player2ScoreText) player2ScoreText.text = "Player 2: " + player2Score.ToString();
            CheckPlayer2Win();
        }
    }

    public void ResetScores()
    {
        player1Score = 0;
        player2Score = 0;

        if (player1ScoreText) player1ScoreText.text = "Player 1: " + player1Score.ToString();
        if (player2ScoreText) player2ScoreText.text = "Player 2: " + player2Score.ToString();
    }

    private void CheckPlayer1Win()
    {
        if (player1Score >= winningScore)
        {
            WinnerPlayer1.SetActive(true);
            StartCoroutine(LoadMenuAfterDelay());
        }
    }

    private void CheckPlayer2Win()
    {
        if (player2Score >= winningScore)
        {
            WinnerPlayer2.SetActive(true);
            StartCoroutine(LoadMenuAfterDelay());
        }
    }

    private IEnumerator LoadMenuAfterDelay()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        Destroy(this.gameObject);
    }
}
