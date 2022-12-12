using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;
    //public AudioSource music;

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        //music.Stop();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        //music.UnPause();
    }

    public void OptionsMenu()
    {
        
    }
    
    public void Home(int ID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(ID);
    }
}
