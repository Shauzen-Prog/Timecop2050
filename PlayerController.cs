using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOld 
{
    DragFingerMove _dragFingerMove;

    public PlayerControllerOld(DragFingerMove dragFingerMove)
    {
        _dragFingerMove = dragFingerMove;
    }

    public void OnUpdate()
    {
        _dragFingerMove.DragMove();
    }
}
