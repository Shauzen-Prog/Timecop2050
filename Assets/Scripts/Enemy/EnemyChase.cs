using UnityEngine;

public class EnemyChase : Entity
{
    public EnemyFlyweightSO basicEnemySO;

    private Animator _animator;
    private Vector3 _velocity;
    private Transform _target;
    
    
    private void Start()
    {
        maxHealth = basicEnemySO.maxHealth;
        
        _animator = GetComponent<Animator>();
        SoundManager.instance.Play(TypesSFX.Second, basicEnemySO.movementSound);
        SoundManager.instance.ChangeVolumeAllSounds(0.1f);
    }
    private void Update()
    {
        ApplyForce(Seek());
        MoveToPosition();
    }

    private Vector3 Seek()
    {
        _target = PlayerModel.instance.GetTransform();
        var desired = _target.transform.position - transform.position;
        desired.Normalize();
        desired *= basicEnemySO.maxSpeed;

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, basicEnemySO.maxForce);

        return steering;
    }

    private void ApplyForce(Vector3 force)
    {
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity + force, basicEnemySO.maxSpeed);
    }

    private void MoveToPosition()
    {
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        var hittableGameObject = other.GetComponent<IHittable>();
        var player = other.GetComponent<PlayerModel>();
        
        if (hittableGameObject != null && other.gameObject.layer != basicEnemySO.enemyLayerMask && player != null)
        {
            hittableGameObject.TakeDamage(basicEnemySO.damage);
            Die();
        }
    }

    public void ExplosionEnable()
    {
        Instantiate(basicEnemySO.explotionGO, transform.position, transform.rotation);
    }
    
    public override void Die()
    {
        _animator.SetTrigger("Die");
        ExplosionEnable();
        SoundManager.instance.Play(TypesSFX.Primary, basicEnemySO.dieSound);
        SpawnHeal();
        base.Die();
    }

    private void SpawnHeal()
    {
        if ((basicEnemySO.chance = Random.Range(0,5)) == basicEnemySO.probabilityToSpawnHealOnDie)
            Instantiate(basicEnemySO.heal, transform.position, Quaternion.identity);
    }
}

