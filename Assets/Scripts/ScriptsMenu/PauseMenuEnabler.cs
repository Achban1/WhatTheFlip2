using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuEnabler : MonoBehaviour
    
{
    public static bool Paused = false;
    public GameObject PauseMenu;

    public GameObject ScoreManager;

    void Start()
    {
        Paused = false;
        Time.timeScale = 1;
        //ScoreManager = GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        Paused = false;

    }
    public void Pause()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        Paused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
