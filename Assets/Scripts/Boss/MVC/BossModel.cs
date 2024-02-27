using System;
using UnityEngine;
using UnityEngine.UI;

public class BossModel : Entity
{
    private HealthBarBehaviour _healthBar;
    public Animator anim;
    public Slider bossSlider;
    [SerializeField] private Vector3 _offsetPosition;
    private BossModelView _bossModelView;
    private BossController _bossController;
    [SerializeField] private BulletHellWeapon bulletHellWeapon;

    public Transform[] waypoints;
    private Rigidbody _rb;
    public float speed;
    private int _curWaypoint;
    private bool _patrol = true;
    private Vector3 _moveDirection;
    private Vector3 _velocity;

    public AudioClip dieSound;
    public AudioClip floatSound;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        var transform1 = transform;
        _bossModelView = new BossModelView(anim ,transform1, bossSlider, _offsetPosition);
        _bossController = new BossController(transform1, waypoints, _rb, speed, _curWaypoint, _patrol, _moveDirection, _velocity);
        
        _bossModelView.OnStart();
        
        actualHealth = maxHealth;
        SoundManager.instance.Play(TypesSFX.Second, floatSound);
    }

    private void Update()
    {
        _bossModelView.OnUpdate();
        _bossController.UpdateController();
        //_healthBar.SetHealth(actualHealth, maxHealth);
    }

    public override void TakeDamage(float dmg)
    {
        if (dmg <= 0) return;
        
        actualHealth -= dmg;
        EventManager.Trigger("ChangeHealthBoss", actualHealth);
        EventManager.Trigger("TriggerAnimBoss", "TakeDamage");

        if (!(actualHealth <= 0)) return;
        
        EventManager.Trigger("TriggerAnimBoss", "Die");
        bulletHellWeapon.canShoot = false;
    }

    public override void Heal(float healAmount)
    {
        if(actualHealth >= maxHealth) return;

        actualHealth += healAmount;
        EventManager.Trigger("ChangeHealthBoss", actualHealth);
        EventManager.Trigger("TriggerAnimBoss", "HealAnim");
        
    }
    public override void Die()
    {
        SoundManager.instance.Play(TypesSFX.Primary, dieSound);
        SceneManagement.instance.LoadScene(6);
        base.Die();
    }

    private void OnDisable()
    {
        _bossModelView.OnDisable();
    }
}
