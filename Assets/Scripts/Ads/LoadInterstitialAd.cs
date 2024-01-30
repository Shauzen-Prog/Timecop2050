using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadInterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsListener
{
    public string androidAdUnitId;
    public string androidGameId;
    public string iosAdUnitId;
    public string iosGameId;

    public Button restartWithPowerUpButton;

    private string _gameId;
    private string _adUnitId;

    public bool testMode = true;

    public LoseMenuManager loseMenuManager;
    
    private void Awake()
    {
#if UNITY_ANDROID
        _adUnitId = androidAdUnitId;
        _gameId = androidGameId;
#elif UNITY_IOS
        _adUnitId = iosAdUnitId;
        _gameId = iosGameId;
#endif
    }

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameId, testMode);
    }

    private void Update()
    {
        restartWithPowerUpButton.interactable = Advertisement.isInitialized;
    }

    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);

        if (Advertisement.IsReady(_adUnitId))
        {
            Advertisement.Show(_adUnitId);
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }
    
    private void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }
    
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        
    }

    public void OnUnityAdsShowStart(string placementId)
    {
       
    }

    public void OnUnityAdsShowClick(string placementId)
    {
       
    }
    
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
        ShowAd();
    }

    public void OnUnityAdsDidError(string message)
    {
       
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        JsonManager.instance.data.activeAllMenuPowerUps = false;
        JsonManager.instance.data.startingPowerUp = null;
        JsonManager.instance.Save();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {
            //Si se ve el Ad
            loseMenuManager?.WatchAdFinished();
            JsonManager.instance.data.activeAllMenuPowerUps = true;
        }
        else if (showResult == ShowResult.Skipped)
        {
            //Si se saltea el Ad
            loseMenuManager?.WatchAdFinished();

            var randomPowerUp = Random.Range(1, 4);

            Debug.Log(randomPowerUp);
            
            JsonManager.instance.data.randomPowerUpActive = randomPowerUp;

            JsonManager.instance.Save();

        }
    }
}
