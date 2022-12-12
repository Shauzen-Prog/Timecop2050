using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserverPoints 
{
    void ReceiveCall(UtilsPoints.Actions actions);
}
