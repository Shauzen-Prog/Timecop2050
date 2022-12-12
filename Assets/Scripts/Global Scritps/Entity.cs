using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (dmg > 0)
        {
            actualHealth -= dmg;
            if (actualHealth <= 0)
            {
                Die();
            }
        }
    }
   
    public virtual void Heal(float heal)
    {
        if (heal > 0)
        {
            actualHealth += heal;

            if (actualHealth >= maxHealth)
            {
                actualHealth = maxHealth;
            }
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

}
