using UnityEngine;

public class EnemyChase : Entity
{
    private Vector3 _velocity;
    private Transform _target;
    public float maxSpeed;
    public float maxForce;
    public float damage;
    public int probability = 1;
    public HealItem heal;
    public LayerMask enemyLayerMask;
    private int _chance;

    public AudioClip movementSound;
    public AudioClip dieSound;

    // Update is called once per frame

    private void Start()
    {
        SoundManager.instance.Play(TypesSFX.Second, movementSound);
        SoundManager.instance.ChangeVolumeAllSounds(0.1f);
    }
    private void Update()
    {
        ApplyForce(Seek());
        MoveToPosition();
    }

    private Vector3 Seek()
    {
        _target = PlayerModel.instance.GetTrasnform();
        var desired = _target.transform.position - transform.position;
        desired.Normalize();
        desired *= maxSpeed;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        return steering;
    }

    private void ApplyForce(Vector3 force)
    {
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity + force, maxSpeed);
    }

    private void MoveToPosition()
    {
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        var hittablegameObject = other.GetComponent<IHittable>();
        var player = other.GetComponent<PlayerModel>();
        
        if (hittablegameObject != null && other.gameObject.layer != enemyLayerMask && player != null)
        {
            hittablegameObject.TakeDamage(damage);
            SpawnHeal();
            Die();
        }
    }

    public override void Die()
    {
        SoundManager.instance.Play(TypesSFX.Primary, dieSound);
        base.Die();
    }

    private void SpawnHeal()
    {
        if ((_chance = Random.Range(0,5)) == probability)
            Instantiate(heal, transform.position, Quaternion.identity);
    }
}

