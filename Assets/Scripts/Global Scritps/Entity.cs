using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IHittable, IObservable
{
    public float maxHealth;
    public float actualHealth;
    
    protected List<IObserver> _observers = new List<IObserver>();
    
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

    public void Subscribe(IObserver obs)
    {
        if (!_observers.Contains(obs))
        {
            _observers.Add(obs);
        }
    }

    public void UnSubscribe(IObserver obs)
    {
        if (_observers.Contains(obs))
        {
            _observers.Remove(obs);
        }
    }

    public void NotifyToObservers(EventEnum eventEnum, params object[] parameters)
    {
        for (int i = 0; i < _observers.Count; i++)
        {
            _observers[i].Notify(eventEnum, (float)parameters[0]);
        }
    }
}
