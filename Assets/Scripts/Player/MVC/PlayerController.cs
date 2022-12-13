using System;
using UnityEngine;

public enum TypeOfController
{
    DragMove,
    TouchToGo,
    NoControls
}

public class PlayerController 
{
    private DragFingerMove _dragFingerMove;

    private Action _ArtificialUpdate;

    public PlayerController(Transform playerTransform)
    {
        _dragFingerMove = new DragFingerMove(playerTransform);
    }

    public void OnAwake()
    {
        _ArtificialUpdate = _dragFingerMove.DragMove;
    }
    
    public void OnUpdate()
    {
        _ArtificialUpdate();
    }

    public void ChangeController(TypeOfController typeOfController)
    {
        switch(typeOfController)
        {
            case TypeOfController.DragMove:
                _ArtificialUpdate = _dragFingerMove.DragMove;
                break;

            case TypeOfController.TouchToGo:
                _ArtificialUpdate = TouchToGo;
                break;

            case TypeOfController.NoControls:
                _ArtificialUpdate = () => { };
                break;
        }
    }

    private void TouchToGo()
    {
        Debug.Log("Touch To Go");
    }
}
