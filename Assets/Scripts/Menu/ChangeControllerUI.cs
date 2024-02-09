using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeControllerUI : MonoBehaviour
{
    public Button DragAndDropButton;
    public Button JoystickButton;
    public GameObject JoystickUI;
    
    // Start is called before the first frame update
    void Start()
    {
        JoystickButton.interactable = true;
        DragAndDropButton.interactable = false;
    }

    public void ControllerToChangeUI(int typeOfController)
    {
        switch (typeOfController)
        {
            // Si toca drag and move
            case 0:
                ChangeInteractableButton();
                JoystickUI.transform.localScale = new Vector3(0, 0, 0);
            
                EventManager.Trigger("ChangeController", TypeOfController.DragMove);
                break;
            // Si toca Joystick
            case 1:
                ChangeInteractableButton();
                JoystickUI.transform.localScale = new Vector3(1f, 1f, 1f);
            
                EventManager.Trigger("ChangeController", TypeOfController.Joystick);
                break;
        }
    }

    private void ChangeInteractableButton()
    {
        JoystickButton.interactable = !JoystickButton.interactable;
        DragAndDropButton.interactable = !DragAndDropButton.interactable;
    }
}
