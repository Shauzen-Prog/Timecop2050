using System;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu;
    public GameObject pauseBackgroundImage;
    private bool isPauseMenuActive;
    //public AudioSource music;

    private void Start()
    {
        isPauseMenuActive = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPauseMenuActive = !isPauseMenuActive;
            PauseGame(isPauseMenuActive);
        }
        
    }

    public void PauseGame(bool status)
    {
        if (status)
        {
            pauseMenu.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Time.timeScale = 0;
            pauseBackgroundImage.SetActive(true);   
        }
        else
        {
            pauseMenu.transform.localScale = Vector3.zero;
            Time.timeScale = 1;
            pauseBackgroundImage.SetActive(false);  
        }
        
        //music.Stop();
    }

    public void BackToMenu(int sceneToLoad)
    {
        SceneManagement.instance.LoadScene(sceneToLoad);
    }
    
}
