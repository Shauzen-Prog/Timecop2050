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

    public BossModelView(Animator anim ,Transform userTransform, Slider sliderBar, Vector3 offset)
    {
        _anim = anim;
        _userTransform = userTransform;
        _sliderBar = sliderBar;
        _offset = offset;
    }

    public void OnStart()
    {
        EventManager.Suscribe("ChangeHealthBoss",ChangeHealth);
        EventManager.Suscribe("TriggerAnimBoss",TriggerAnim);
    }
    
    private void ChangeHealth(params object[] parameters)
    {
        var health = (float)parameters[0];
        
        _sliderBar.value = health / 100f;
    }

    public void OnUpdateChangeTransformPosition()
    {
        //_sliderBar.transform.position = Camera.main.WorldToScreenPoint(_userTransform.parent.position + _offset);
    }

    public void OnUpdate()
    {
        OnUpdateChangeTransformPosition();
    }

    private void TriggerAnim(params object[] parameters)
    {
        var triggerAnim = (string)parameters[0];
        
        _anim.SetTrigger(triggerAnim);
    }

    public void OnDisable()
    {
        EventManager.UnSuscribe("ChangeHealthBoss",ChangeHealth);
        EventManager.UnSuscribe("TriggerAnimBoss",TriggerAnim);
    }
}
