using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private TypeOfController typeOfController;
 
    public void ChangeControl()
    {
        PlayerModel.instance._playerController.ChangeController(typeOfController);
    }


}
