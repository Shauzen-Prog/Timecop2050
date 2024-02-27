using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PlayerLoop;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance;

    public bool isReadyToTranslate;
    public Language selectedLanguage;
    public string externalURL = "https://drive.google.com/uc?export=download&id=1y5ErPPMOHxF-sdoDPsf_xWf9eXHK3GJZ";

    Dictionary<Language, Dictionary<string, string>> _languageManager;

    public event Action OnUpdate;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    
    private void Start()
    {
        isReadyToTranslate = false;
        StartCoroutine(DownloadCSV(externalURL));
    }

    public void ChangeLanguage()
    {
        selectedLanguage = selectedLanguage == Language.eng ? Language.spa : Language.eng;

        OnUpdate?.Invoke();
    }

    public string GetTranslate(string id)
    {
        if (!_languageManager[selectedLanguage].ContainsKey(id))
            return "Error 404: Not found";
        else
            return _languageManager[selectedLanguage][id];
    }

    IEnumerator DownloadCSV(string url)
    {
        var www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        _languageManager = LanguageU.LoadCodex(www.downloadHandler.text);
        
        isReadyToTranslate = true;
        OnUpdate?.Invoke();

    }
    
}

public enum Language
{
    eng,
    spa
}
