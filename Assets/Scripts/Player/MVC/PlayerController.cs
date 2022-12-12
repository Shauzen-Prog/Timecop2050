using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController 
{
    private DragFingerMove _dragFingerMove;
    public PlayerController(Transform playerTransform)
    {
        _dragFingerMove = new DragFingerMove(playerTransform);
    }
    
    public void UpdateControllers()
    {
        _dragFingerMove.DragMove();
    }
}
