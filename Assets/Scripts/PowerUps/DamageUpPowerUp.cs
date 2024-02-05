using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpPowerUp : PowerUp
{
    public float speed;
    public int damageUp;

    private void Update()
    {
        transform.position += -transform.up * (speed * Time.deltaTime);
    }

    public int ReturnDamageNewValue()
    {
        return damageUp;
    }
}
