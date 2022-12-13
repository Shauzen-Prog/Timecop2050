using UnityEngine;
using UnityEngine.UI;

public class PlayerModel : Entity, IObserverPoints
{
    public static PlayerModel instance;
    public PlayerController _playerController;
    public HealthBarBehaviour _healthBar;
    private PlayerView _playerView;
    public Vector3 offsetPosition;
    public Slider barHealthSlider;

    private void Awake()
    {
        instance = this;
        _playerController = new PlayerController(transform);
        _playerView = new PlayerView(transform, barHealthSlider, offsetPosition);

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
        _playerView.OnUpdate();
        _playerView.OnUpdateChangeHealth(actualHealth / 100);

        if (Input.GetKeyDown(KeyCode.F))
        {
            Die();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10f);
        }
    }

    public void ReceiveCall(UtilsPoints.Actions action)
    {
        if (action == UtilsPoints.Actions.CompleteChargePoints)
            Debug.Log("PowerUp");
        else
            Debug.Log("Lost PowerUp");
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
            
        if (actualHealth <= 0)
            Die();
    }

    public Transform GetTrasnform()
    {
        return transform;
    }
}