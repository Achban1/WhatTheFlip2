using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //start menu
    //public float musicSlider;
    //public float fxSlider;
    public GameObject image;
    public GameObject Logo;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 2);
    }

    public void Controls()
    {
        image.SetActive(true);
        Logo.SetActive(false);
    }
    public void ControlsQuit()
    {
        image.SetActive(false);
        Logo.SetActive(true);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    //public void MusicSlider(float volume)
    //{
    //    musicSlider = volume*100f;
    //    PlayerPrefs.SetFloat("musicvolume", musicSlider);
    //}
    //public void FxSlider(float volume)
    //{
    //    fxSlider = volume * 100f;
    //    PlayerPrefs.SetFloat("fxvolume", fxSlider);
    //}

}
