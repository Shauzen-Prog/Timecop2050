using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenuManager : MonoBehaviour
{
    [Header("Menus")] 
    public GameObject LoseScreen;
    public GameObject AdMenu;
    public GameObject powerUpChooseMenu;
    
    [Header("Others Variables")] 
    private Vector3 makeSmall = Vector3.zero;
    private Vector3 makeNormalSize = Vector3.one;


    public void OpenAdMenu()
    {
        LoseScreen.transform.localScale = makeSmall;
        AdMenu.transform.localScale = makeNormalSize;
    }

    public void WatchAdFinished()
    {
        LoseScreen.transform.localScale = makeSmall;
        AdMenu.transform.localScale = makeSmall;
        powerUpChooseMenu.transform.localScale = makeNormalSize;
    }

    public void CloseAdMenu()
    {
        LoseScreen.transform.localScale = makeNormalSize;
        AdMenu.transform.localScale = makeSmall;
    }
    
    
}
