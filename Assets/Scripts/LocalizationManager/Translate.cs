using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translate : MonoBehaviour
{
    public string ID;
    public TextMeshProUGUI myText;

    private void Start()
    {
        LanguageManager.instance.OnUpdate += ChangeLang;
    }

    void ChangeLang()
    {
        myText.text = LanguageManager.instance.GetTranslate(ID);
    }
}
