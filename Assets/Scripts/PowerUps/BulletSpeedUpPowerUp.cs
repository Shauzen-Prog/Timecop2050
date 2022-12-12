using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletSpeedUpPowerUp : PowerUp
{
    public float speed;
    public float bulletSpeedUp;

    private void Update()
    {
        transform.position += -transform.up * speed * Time.deltaTime;
    }

    public float ReturnBulletSpeedUpValue()
    {
        return bulletSpeedUp;
    }
}
