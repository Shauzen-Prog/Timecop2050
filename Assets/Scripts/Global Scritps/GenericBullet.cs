using UnityEngine;

public abstract class GenericBullet : MonoBehaviour
{
    public Pool<GenericBullet> pool; 
    public float bulletDamage = 1f;
    public float bulletMaxDuration = 1f;
    public float speed = 1f;
    public Vector3 bulletDirection;
    public int noHittableLayer;
    private float _counterBulletDuration;

    public AudioClip shootSound;

    public virtual void Start()
    {
        SoundManager.instance.Play(TypesSFX.Primary, shootSound);
    }

    public virtual void Update()
    {
        MoveBullet();
        ReturnBulletToPoolBasedOnInternalCounter();
    }

    public virtual void SetBulletDirection(Vector3 newBulletDirection)
    {
        bulletDirection = newBulletDirection;
    }

    private void ReturnBulletToPoolBasedOnInternalCounter()
    {
        _counterBulletDuration += Time.deltaTime;

        if (_counterBulletDuration >= bulletMaxDuration)
        {
            _counterBulletDuration = 0;
            ReturnBulletToPool();
        }
    }

    public virtual void MoveBullet()
    {
        transform.position += bulletDirection * (speed * Time.deltaTime);  
    }

    public virtual void ReturnBulletToPool()
    {
        pool.ReturnObj(this);
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        IHittable hittedgameObject = other.GetComponent<IHittable>();

        if (other.gameObject.layer != noHittableLayer)
        {
            hittedgameObject?.TakeDamage(bulletDamage, null);
            ReturnBulletToPool();
        }
    }

    public static void Active(GenericBullet b)
    {
        b.gameObject.SetActive(true);
    }
    public static void Disable(GenericBullet b)
    {
        b.gameObject.SetActive(false);
    }
}
