using UnityEngine;

public class EnemyBullet : GenericBullet
{
    public override void OnTriggerEnter(Collider other)
    {
        var hittableGameObject = other.GetComponent<IHittable>();

        if (hittableGameObject == null || other.gameObject.layer == 9) return;
        hittableGameObject.TakeDamage(bulletDamage, null);
        ReturnBulletToPool();
    }
}

