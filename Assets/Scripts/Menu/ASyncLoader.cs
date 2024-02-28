using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ASyncLoader : MonoBehaviour
{
    public static ASyncLoader instance;
    
    [Header("If Need Pass To Next Level")] 
    public bool isOnLevelCompleted;
    public bool isOnLoseLevel;
    public bool isBackToMainMenu;
    
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;
    
    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void LevelToLoad(int levelToLoad)
    {
        ActiveAndDeactiveMenus();

        StartCoroutine(LoadLevelAsync(levelToLoad));
    }
    
    public void LoadLevelLastScene()
    {
        ActiveAndDeactiveMenus();

        StartCoroutine(LoadLevelAsync(JsonManager.instance.data.lastScene));
    }

    public void PassLevelBtn()
    {
        ActiveAndDeactiveMenus();
        StartCoroutine(LoadLevelAsync(JsonManager.instance.data.sceneToLoad));
    }

    public void BackToMenuBtn()
    {
        ActiveAndDeactiveMenus();
        StartCoroutine(LoadLevelAsync(0));
    }

    public void PassLevel()
    {
        ActiveAndDeactiveMenus();
        isOnLevelCompleted = true;
        StartCoroutine(LoadLevelAsync(4));
    }

    public void LoseLevel()
    {
        ActiveAndDeactiveMenus();
        isOnLoseLevel = true;
        var sceneLose = 5;
        StartCoroutine(LoadLevelAsync(sceneLose));
    }

    public void ActiveAndDeactiveMenus()
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
    }
    
    private IEnumerator LoadLevelAsync(int levelToLoad)
    {
        if(SoundManager.instance != null)
            SoundManager.instance.StopAllSounds();
        
       // if (isOnLevelCompleted)
       // {
       //     levelToLoad = JsonManager.instance.data.sceneToLoad;
       // }
//
       // if (isOnLoseLevel)
       // {
       //     levelToLoad = JsonManager.instance.data.lastScene;
       // }
//
       // if (isBackToMainMenu)
       // {
       //     levelToLoad = 0;
       // }
       // Debug.Log(levelToLoad);
        
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Single);

        while (!loadOperation.isDone)
        {
            var progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
