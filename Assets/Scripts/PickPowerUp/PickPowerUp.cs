using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPowerUp : MonoBehaviour
{
    public void ChoosePowerUp(string powerUp)
    {
        Debug.Log(powerUp);
        JsonManager.instance.data.startingPowerUp = powerUp;
        JsonManager.instance.Save();
    }
}
