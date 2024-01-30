using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translate : MonoBehaviour
{
    public string ID;
    public TextMeshProUGUI myText;

    private void Awake()
    {
       
    }

    private void Start()
    {
        if (LanguageManager.instance.isReadyToTranslate)
        {
            ChangeLang();
        }
        LanguageManager.instance.OnUpdate += ChangeLang;
    }

    void ChangeLang()
    {
        myText.text = LanguageManager.instance.GetTranslate(ID);
    }
    
}
