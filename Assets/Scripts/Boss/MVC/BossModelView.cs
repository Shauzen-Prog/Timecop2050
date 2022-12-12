using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossModelView
{
    private Animator _anim;
    private Transform _userTransform;
    private Slider _sliderBar;
    private Vector3 _offset;

    public BossModelView(Animator anim ,Transform userTrasnform, Slider sliderBar, Vector3 offset)
    {
        _anim = anim;
        _userTransform = userTrasnform;
        _sliderBar = sliderBar;
        _offset = offset;
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

    public void SetTriggerAnim(string triggerAnim)
    {
        _anim.SetTrigger(triggerAnim);
    }
}
