using UnityEngine;
using UnityEngine.UI;

public class PlayerView
{
    HealthBarBehaviour _healthBar;
    Transform _playerTransform;
    Slider _sliderBar;
    Vector3 _offSetPosition;
    private readonly float _maxHealth;
    private readonly float _actualHealth;

    public PlayerView(Transform playerTransform, Slider healthBarSlider, Vector3 offsetPosition, 
        float actualHealth, float maxHealth)
    {
        _playerTransform = playerTransform;
        _sliderBar = healthBarSlider;
        _offSetPosition = offsetPosition;
        _actualHealth = actualHealth;
        _maxHealth = maxHealth;
    }
    public void OnStart()
    {
        //_sliderBar.transform.position = _offSetPosition;
        EventManager.Suscribe("ChangeHealthPlayer", UpdateHealBar);
        _sliderBar.gameObject.SetActive(true);
        _sliderBar.value = _maxHealth;
    }

    public void OnUpdateChangeTransformPosition()
    {
        _sliderBar.transform.position = Camera.main.WorldToScreenPoint(_playerTransform.position + _offSetPosition);
    }

    private void UpdateHealBar(params object[] parameters)
    {
        var health = (float)parameters[0];
        
        _sliderBar.value = health / 100f;
    }
    
   
    
}
