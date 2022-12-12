using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance;

    public Language selectedLanguage;
    public string externalURL = "https://drive.google.com/uc?export=download&id=19NhRbwhZaO9f2SgsxwBa5yecQDZgoRXs";

    Dictionary<Language, Dictionary<string, string>> _languageManager;

    public event Action OnUpdate;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
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

        OnUpdate?.Invoke();

    }
}

public enum Language
{
    eng,
    spa
}
