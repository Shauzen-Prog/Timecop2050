using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IHittable
{
    public float maxHealth;
    public float actualHealth;
    
    private void Start()
    {
        actualHealth = maxHealth;
    }
    public virtual void TakeDamage(float dmg)
    {
        if(dmg <= 0) return;
        
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
    
    
    public virtual void Die()
    {
        Destroy(gameObject);
    }
    
}
