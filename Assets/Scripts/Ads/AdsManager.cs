using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public string androidGameId;
    public string iosGameId;
    public bool isTestingMode = true;

    private string gameId;

    private void Awake()
    {
        InitializeAds();
    }

    private void InitializeAds()
    {
        
#if UNITY_IOS
        gameId = iosGameId;
#elif UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_EDITOR
        gameId = androidGameId; //for testing
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, isTestingMode, this);
        }
    }
}
