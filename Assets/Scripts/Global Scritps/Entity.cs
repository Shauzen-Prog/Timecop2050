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
   
    public void Heal(float heal)
    {
        if(heal <= 0) return;
        
        actualHealth += heal;
        
        if (actualHealth >= maxHealth)
            actualHealth = maxHealth;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

}
