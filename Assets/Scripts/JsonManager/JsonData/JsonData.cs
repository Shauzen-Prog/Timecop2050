using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JsonData 
{
    public bool[] completeLevel = new bool[4];

    public bool activeAllMenuPowerUps;

    public int lastScene;

    public int sceneToLoad;

    public string[] randomPowerUpString = {"DamageUp" , "SpeedShotUp", "None"};

    public int randomPowerUpActive;

    public string startingPowerUp;

    public TypeOfController typeOfController;
}
