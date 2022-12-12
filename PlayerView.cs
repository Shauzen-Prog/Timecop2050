using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView 
{
    private Transform _userTransform;
    private Slider _sliderBar;
    private Vector3 _offset;

    public PlayerView(Transform userTrasnform, Slider sliderBar, Vector3 offset)
    {
        _userTransform = userTrasnform;
        _sliderBar = sliderBar;
        _offset = offset;
    }

    public void OnStart()
    {
        _sliderBar.transform.position = _offset;
    }

    public void ChangeHealth(float health, float maxHealth)
    {
        _sliderBar.gameObject.SetActive(health < maxHealth);
        _sliderBar.value = health;
        _sliderBar.maxValue = maxHealth;
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
