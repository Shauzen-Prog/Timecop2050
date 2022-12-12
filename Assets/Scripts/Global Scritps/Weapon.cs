using UnityEngine;
using UnityEngine.Serialization;

public abstract class Weapon : MonoBehaviour
{
    public GenericBullet bullet;
    protected Pool<GenericBullet> pool;
    public int startingBullets = 2;
    public float time = 1f;
    public float repeatRate = 0.5f;
    private const string shootingNameOnString = "Shooting";

    public virtual void Start()
    {
        pool = new Pool<GenericBullet>(Factory, GenericBullet.Disable, GenericBullet.Active, startingBullets);
        InvokeRepeating(shootingNameOnString, time, repeatRate);
    }

    protected virtual GenericBullet Factory()
    {       
        return Instantiate(bullet, transform.position, transform.rotation);
    }
    public virtual void Shooting()
    {
        GenericBullet instantiatedBullet = pool.AcquireObj();
        SetPositionAndRotationOfThisBullet(instantiatedBullet);
    }

    protected virtual void SetPositionAndRotationOfThisBullet(GenericBullet bulletToChange)
    {
        var bulletTransform = bulletToChange.transform;
        bulletTransform.position = transform.position;
        bulletTransform.rotation = transform.rotation;
        bulletToChange.pool = pool;
    }
}
