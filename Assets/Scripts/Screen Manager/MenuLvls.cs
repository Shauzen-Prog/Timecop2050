using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLvls : MonoBehaviour
{
    public GameObject lvls;
    public GameObject menu;
    public GameObject backGround;
    public GameObject credits;
    public GameObject controls;
    
    private Vector3 makeSmall = new Vector3(0.001f, 0.001f, 0.001f);
    private Vector3 makeNormalSize = new Vector3(0.3f, 0.3f, 0.3f);
    
    public void TurnOnLvls()
    {
        menu.transform.localScale = makeSmall;
        lvls.transform.localScale = makeNormalSize;
        backGround.SetActive(true);
    }

    public void CloseLvls()
    {
        menu.transform.localScale = makeNormalSize;
        lvls.transform.localScale = makeSmall;
        backGround.SetActive(false);
    }

    public void TurnOnCredits()
    {
        menu.transform.localScale = makeSmall;
        credits.transform.localScale = makeNormalSize;
    }

    public void CloseCredits()
    {
        menu.transform.localScale = makeNormalSize;
        credits.transform.localScale = makeSmall;
    }

    public void TurnOnControls()
    {
        menu.transform.localScale = makeSmall;
        controls.transform.localScale = makeNormalSize;
    }

    public void CloseControls()
    {
        menu.transform.localScale = makeNormalSize;
        controls.transform.localScale = makeSmall;
    }
}
