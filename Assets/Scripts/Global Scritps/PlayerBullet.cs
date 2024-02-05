using UnityEngine;

public class PlayerBullet : GenericBullet
{
    public Material changeColorPowerUpDamage;
    public Material changeColorPowerUpSpeed;

    public override void OnTriggerEnter(Collider other)
    {
        var hittedgameObject = other.GetComponent<IHittable>();
        var damageUp = other.GetComponent<DamageUpPowerUp>();
        var speedUp = other.GetComponent<BulletSpeedUpPowerUp>();

        if (hittedgameObject != null)
        {
            hittedgameObject.TakeDamage(bulletDamage, null);
            ReturnBulletToPool();
        }

        if (damageUp != null)
        {
            bulletDamage += damageUp.ReturnDamageNewValue();
            gameObject.GetComponent<Renderer>().material = changeColorPowerUpDamage;
            Destroy(damageUp.gameObject);
            ///PowerUpSpawner.instance.powerUpCount--;
            ReturnBulletToPool();
        }

        if (speedUp != null)
        {
            speed += speedUp.ReturnBulletSpeedUpValue();
            gameObject.GetComponent<Renderer>().material = changeColorPowerUpSpeed;
            Destroy(speedUp.gameObject);
            //PowerUpSpawner.instance.powerUpCount--;
            ReturnBulletToPool();
        }
    }
}
