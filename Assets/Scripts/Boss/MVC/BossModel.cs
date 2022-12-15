using UnityEngine;
using UnityEngine.UI;

public class BossModel : Entity
{
    private HealthBarBehaviour _healthBar;
    public Animator anim;
    public Slider bossSlider;
    [SerializeField] private Vector3 offsetPosition;
    private BossModelView _bossModelView;
    private BossController _bossController;

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
        _bossModelView = new BossModelView(anim ,transform, bossSlider, offsetPosition);
        _bossController = new BossController(transform, waypoints, _rb, speed, _curWaypoint, _patrol, _moveDirection, _velocity);
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
        _bossModelView.SetTriggerAnim("TakeDamage");
        _bossModelView.ChangeHealth(actualHealth, maxHealth);
        if (actualHealth <= 0)
        {
            _bossModelView.SetTriggerAnim("Die");
        }
    }
    public override void Die()
    {
        SoundManager.instance.Play(TypesSFX.Primary, dieSound);
        SceneManagement.instance.LoadScene(6);
        base.Die();
    }

}
