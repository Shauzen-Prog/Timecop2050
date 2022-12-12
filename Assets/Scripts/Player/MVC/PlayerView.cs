using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView
{
    HealthBarBehaviour _healthBar;
    Transform _playerTransform;
    Slider _sliderBar;
    Vector3 _offSetPosition;

    public PlayerView(Transform playerTransform, Slider healthBarSlider, Vector3 offsetPosition)
    {
        _playerTransform = playerTransform;
        _sliderBar = healthBarSlider;
        _offSetPosition = offsetPosition;
    }
    public void OnStart()
    {
        //_sliderBar.transform.position = _offSetPosition;
        _sliderBar.gameObject.SetActive(true);
    }

    public void OnUpdateChangeHealth(float health)
    {
        _sliderBar.value = health;
    }

    public void OnUpdateChangeTransformPosition()
    {
        //_sliderBar.transform.position = Camera.main.WorldToScreenPoint(_userTransform.parent.position + _offset);
    }

    public void OnUpdate()
    {
        OnUpdateChangeTransformPosition();
    }
}
