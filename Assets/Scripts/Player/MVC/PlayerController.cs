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
    private Transform _playerTransform;
    private DragFingerMove _dragFingerMove;

    private Action _ArtificialUpdate;

    public PlayerController(Transform playerTransform)
    {
        _playerTransform = playerTransform;
        _dragFingerMove = new DragFingerMove(playerTransform);
    }

    public void OnAwake()
    {
#if UNITY_EDITOR

        _ArtificialUpdate = MoveWASDController;
        
        return;
        
#endif
        _ArtificialUpdate = _dragFingerMove.DragMove;
        ChangeController(TypeOfController.DragMove);
   
        
        
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

    #if UNITY_EDITOR
        
    private void MoveWASDController()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var _moveVector = new Vector3(horizontal, 0, vertical);
        
        _playerTransform.position += _moveVector * (20 * Time.deltaTime);
    }
        
    #endif
}
