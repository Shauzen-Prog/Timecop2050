using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;

public class AdsManager : MonoBehaviour
{
    public string adID = "Rewarded_Android";
    public GameObject Menu;
    public GameObject LoseMenu;

    public void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Advertisement.Initialize("4492229", true);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            Advertisement.Initialize("4492229", false);
        }
      //StartCoroutine(WaitToShowAd());
    }

    public void WatchAd()
    {
        JsonManager.instance.data.activeAllMenuPowerUps = false;
        JsonManager.instance.data.startingPowerUp = null;
        JsonManager.instance.Save();
        StartCoroutine(WaitToShowAd());
    }

    IEnumerator WaitToShowAd()
    {
        while (!Advertisement.IsReady(adID))
        {
            yield return new WaitForEndOfFrame();
        }

        ShowOptions options = new ShowOptions();
        options.resultCallback = Result;

        Advertisement.Show(adID, options);
    }

    void Result(ShowResult _result)
    {
        Debug.Log(_result.ToString());
        if(_result == ShowResult.Finished)
        {
            //Si se ve el Ad
            Debug.Log("no se salteo");
            LoseMenu.SetActive(false);
            Menu.SetActive(true);
            JsonManager.instance.data.activeAllMenuPowerUps = true;
        }
        else
        {
            //Si se saltea el Ad

            Debug.Log("se salteo");
            LoseMenu.SetActive(false);
            Menu.SetActive(true);

            int randomPowerUp = Random.Range(1, 4);

            Debug.Log(randomPowerUp);


            JsonManager.instance.data.randomPowerUpActive = randomPowerUp;


            JsonManager.instance.Save();
        }
    }
}
