using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    public void LoadScene(int sceneToLoad)
    {
        SceneManagement.instance?.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        SceneManagement.instance?.QuitGame();
    }

    public void LoadLastScene()
    {
        SceneManagement.instance?.LoadLastScene();
    }
    
    public void LoadNextLevel()
    {
        SceneManagement.instance?.LoadNextLevel();
    }
}
