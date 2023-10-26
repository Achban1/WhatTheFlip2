using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    public GameObject escPanel;
    public GameObject escSlider;
    void Start()
    {
        //escSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("volume", 100f);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscPanelVisible();
        }
    }

    private void EscPanelVisible()
    {
        escPanel.SetActive(true);
    }

    public void Resume()
    {
        escPanel.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void EscVolumeSlider()
    {

    }
}
