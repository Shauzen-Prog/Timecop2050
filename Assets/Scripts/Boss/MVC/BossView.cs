using System;
using UnityEngine;
using UnityEngine.UI;

public class BossView : MonoBehaviour, IObserver
{
    private BossModel _bossModel;
    private IObservable _myModelObservable;
    
    public Slider sliderBar;
    
    public AudioClip dieSound;
    public AudioClip moveSound;
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
    private static readonly int Die = Animator.StringToHash("Die");

    public void Start()
    {
        _bossModel = GetComponent<BossModel>();
        _myModelObservable = GetComponent<Entity>();
        _myModelObservable.Subscribe(this);
        SoundManager.instance.Play(TypesSFX.Second, moveSound);
    }
    
    private void ChangeHealth(float health)
    {
        sliderBar.value = health / 100f;
    }

    public void Notify(EventEnum eventEnum, params object[] parameters)
    {
        var health = (float)parameters[0];
        
        switch (eventEnum)
        {
            case EventEnum.TakeDamage:
                ChangeHealth(health);
                _bossModel.anim.SetTrigger(TakeDamage);
                break;
            case EventEnum.Death:
                ChangeHealth(health);
                _bossModel.anim.SetTrigger(Die);
                sliderBar.gameObject.SetActive(false);
                SoundManager.instance.Play(TypesSFX.Second, dieSound);
                break;
        }
    }

    private void OnDisable()
    {
        _myModelObservable.UnSubscribe(this);
    }
}
