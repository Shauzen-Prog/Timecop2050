using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour
{
    private Transform _userTransform;
    private Slider _sliderBar;
    private Vector3 _offset;

    public HealthBarBehaviour(Transform userTransform ,Slider sliderBar, Vector3 offset)
    {
        _userTransform = userTransform;
        _sliderBar = sliderBar;
        _offset = offset;
    }

    public void SetHealth(float health, float maxHealth)
    {
        _sliderBar.gameObject.SetActive(health < maxHealth);
        _sliderBar.value = health;
        _sliderBar.maxValue = maxHealth;
    }

    public void OnUpdateChangeTransformPosition()
    {
        _sliderBar.transform.position = Camera.main.WorldToScreenPoint(_userTransform.parent.position + _offset);
    }
}
