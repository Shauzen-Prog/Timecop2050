using System;
using UnityEngine;
using UnityEngine.UI;

public class BossModel : Entity
{
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody rigidbody;
    private const bool CanShootOnDie = false; 

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        actualHealth = maxHealth;
    }
    
    public override void TakeDamage(float dmg)
    {
        if (dmg <= 0) return;
        
        actualHealth -= dmg;
        NotifyToObservers(EventEnum.TakeDamage, actualHealth);

        if (!(actualHealth <= 0)) return;
        
        NotifyToObservers(EventEnum.Death, actualHealth);
    }

    public override void Die()
    {
        SceneManagement.instance.LoadScene(6);
        base.Die();
    }
}
