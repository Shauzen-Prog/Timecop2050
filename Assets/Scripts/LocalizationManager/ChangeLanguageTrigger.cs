using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanguageTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LanguageManager.instance.ChangeLanguage();
    }

    
}
