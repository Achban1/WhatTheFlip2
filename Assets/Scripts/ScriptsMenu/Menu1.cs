using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Menu1 : MonoBehaviour
{
    //esc menu
    PauseMenuEnabler enabler;
    //public GameObject controlsImage;

    private void Start()
    {
        enabler = GetComponent<PauseMenuEnabler>();
        //controlsImage.SetActive(false);
    }
 
    // Exits the game
    public void Exit()
	{
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    // Loads the Menu
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        enabler.Resume();
    }

    public void Controls()
    {
        //controlsImage.SetActive(true);
    }
    public void ControlsQuit()
    {
        //controlsImage.SetActive(false);
    }
}
