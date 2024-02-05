using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IHittable
{
    public float maxHealth;
    public float actualHealth;

    private Entity ToMakeDamage;
    
    private void Start()
    {
        actualHealth = maxHealth;
        EventManager.Suscribe("TakeDamage", TakeDamageTo);
    }
    public virtual void TakeDamage(float dmg, Entity objective)
    {
        if(dmg <= 0) return;
        
        Debug.Log(objective);
        
        if (ToMakeDamage == null || ToMakeDamage != objective) return;
        
        actualHealth -= dmg;
        
        if (actualHealth <= 0)
            Die();

    }
    
    
    public virtual void Heal(float healAmount)
    {
        if(healAmount <= 0) return;
        
        actualHealth += healAmount;
        
        if (actualHealth >= maxHealth)
            actualHealth = maxHealth;
    }

    private void TakeDamageTo(params object[] parameters)
    {
        ToMakeDamage = (Entity)parameters[0];
        
        
        
    }
    
    
    public virtual void Die()
    {
        Destroy(gameObject);
    }


    public virtual void TakeDamage(float dmg)
    {
        throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        EventManager.UnSuscribe("TakeDamage", TakeDamageTo);
    }
}
