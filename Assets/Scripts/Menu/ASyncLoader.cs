using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ASyncLoader : MonoBehaviour
{
    [Header("If Need Pass To Next Level")] 
    public bool isOnLevelCompleted;
    public bool isOnLoseLevel;
    public bool isBackToMainMenu;
    
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;
    
    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;
    
    public void LoadLevelBtn(int levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelAsync(levelToLoad));
    }
    

    private IEnumerator LoadLevelAsync(int levelToLoad)
    {
        if (isOnLevelCompleted)
        {
            levelToLoad = JsonManager.instance.data.sceneToLoad;
        }

        if (isOnLoseLevel)
        {
            levelToLoad = JsonManager.instance.data.lastScene;
        }

        if (isBackToMainMenu)
        {
            levelToLoad = 0;
        }
        
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
