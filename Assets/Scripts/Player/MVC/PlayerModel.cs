using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModel : Entity, IObserverPoints
{
    public static PlayerModel instance;
    public PlayerController _playerController;
    private PlayerView _playerView;
    public Vector3 offsetPosition;
    public Slider barHealthSlider;
    private Animator _anim;
    private Rigidbody _rigidbody;
    
    [Header("Joystick Variables")]
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private float joystickMoveSpeed;
    
    [Header("Clamp Controller Value")]
    public float minClampX; 
    public float maxClampX; 
    public float minClampY; 
    public float maxClampY;

    private void Awake()
    {
        instance = this;
        _anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerController = new PlayerController(transform ,_rigidbody ,_fixedJoystick ,minClampX, maxClampX, minClampY, maxClampY ,joystickMoveSpeed);
        _playerView = new PlayerView(transform, barHealthSlider, offsetPosition, actualHealth, maxHealth);
        
        _playerController.OnAwake();
    }
    // Start is called before the first frame update
    private void Start()
    {
        ManagerPoints.instance.Subscribe(this);
        _playerView.OnStart();
        actualHealth = maxHealth;
    }

    private void Update()
    {
        _playerController.OnUpdate();
       
        
#if UNITY_EDITOR
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10f);
        }
#endif
    }

    private void FixedUpdate()
    {
        _playerController.OnFixedUpdate();
    }

    public override void Die()
    {
        gameObject.SetActive(false);
        JsonManager.instance.data.lastScene = SceneManagement.ReturnThisSceneIndex();
        JsonManager.instance.Save();
        ASyncLoader.instance.LoseLevel();
    }

    public override void TakeDamage(float dmg)
    {
        if (!(dmg > 0)) return;
        
        actualHealth -= dmg;
        
        EventManager.Trigger("ChangeHealthPlayer", actualHealth);
        
        if (!(actualHealth <= 0)) return;
        Die();
    }

    public override void Heal(float healAmount)
    {
        if(actualHealth >= maxHealth) return;

        actualHealth += healAmount;
        EventManager.Trigger("ChangeHealthPlayer", actualHealth);
        EventManager.Trigger("TriggerAnimPlayer", "HealAnim");
    }


    public void ReceiveCall(UtilsPoints.Actions action)
    {
        if (action == UtilsPoints.Actions.CompleteChargePoints)
            Debug.Log("PowerUp");
        else
            Debug.Log("Lost PowerUp");
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void OnDisable()
    {
        _playerController.OnDisable();
    }
}
