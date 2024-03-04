using System;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(0)]
public class PlayerModel : Entity
{
    public static PlayerModel instance;
    private Animator _anim;
    
    [HideInInspector]
    public new Rigidbody rigidbody;
    
    private void Awake()
    {
        instance = this;
        _anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
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
        ASyncLoader.instance.LoseLevel();
    }

    public override void TakeDamage(float dmg)
    {
        if (!(dmg > 0)) return;
        
        actualHealth -= dmg;
        NotifyToObservers(EventEnum.TakeDamage, actualHealth);
        
        if (!(actualHealth <= 0)) return;
        
        NotifyToObservers(EventEnum.Death, actualHealth);
        Die();
    }

    public override void Heal(float healAmount)
    {
        if(actualHealth >= maxHealth) return;

        actualHealth += healAmount;
        NotifyToObservers(EventEnum.Healing, actualHealth);
    }
    
    public Transform GetTransform()
    {
        return transform;
    }
}
