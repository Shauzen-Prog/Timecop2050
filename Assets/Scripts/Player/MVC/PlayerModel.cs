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
    
    [Header("Clamp Controller Value")]
    public float minClampX; 
    public float maxClampX; 
    public float minClampY; 
    public float maxClampY;

    private void Awake()
    {
        instance = this;
        _anim = GetComponent<Animator>();
        EventManager.Trigger("UpPlayerController",transform ,minClampX, maxClampX, minClampY, maxClampY);
        //_playerController = new PlayerController(transform ,minClampX, maxClampX, minClampY, maxClampY);
        _playerView = new PlayerView(transform, barHealthSlider, offsetPosition, _anim, actualHealth, maxHealth);
        
        
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
        
        EventManager.Trigger("UpdateControllers");

        if (Input.GetKeyDown(KeyCode.F))
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10f);
        }
    }
    
    public override void Die()
    {
        gameObject.SetActive(false);
        JsonManager.instance.data.lastScene = SceneManagement.ReturnThisSceneIndex();
        JsonManager.instance.Save();
        SceneManagement.instance.LoadScene(5);
    }

    public override void TakeDamage(float dmg)
    {
        if (!(dmg > 0)) return;
        
        actualHealth -= dmg;
        
        EventManager.Trigger("ChangeHealthPlayer", actualHealth);
        EventManager.Trigger("TriggerAnimPlayer", "TakeDamage");
        
        if (!(actualHealth <= 0)) return;
        
        EventManager.Trigger("TriggerAnimPlayer", "Die");
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
}
