using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TranslateOnEnable : MonoBehaviour
{
    public string ID;
    public TextMeshProUGUI myText;

    private void OnEnable()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
        
        LanguageManager.instance.OnUpdate += ChangeLang;
    }

    void ChangeLang()
    {
        myText.text = LanguageManager.instance.GetTranslate(ID);
    }
}
