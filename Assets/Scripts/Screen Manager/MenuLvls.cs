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

    public void TurnOnLvls()
    {
        menu.SetActive(false);
        lvls.SetActive(true);
        backGround.SetActive(true);
    }

    public void CloseLvls()
    {
        menu.SetActive(true);
        lvls.SetActive(false);
        backGround.SetActive(false);
    }

    public void TurnOnCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void CloseCredits()
    {
        menu.SetActive(true);
        credits.SetActive(false);
    }

    public void TurnOnControls()
    {
        menu.SetActive(false);
        controls.SetActive(true);
    }

    public void CloseControls()
    {
        menu.SetActive(true);
        controls.SetActive(false);
    }
}
