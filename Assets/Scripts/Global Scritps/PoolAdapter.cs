using System;
using UnityEngine;
using Object = UnityEngine.Object;

public interface IAdapter<in T>
{
    public Action<T> TurnOn();
    public Action<T> TurnOff();
}

public class PoolAdapter<T> : IAdapter<T> where T : Object
{
    private T prefab;
    private GameObject actualprefab;
    private Pool<T> _pool;
    
    public PoolAdapter(T gameObject, int AmountStart)
    {
        prefab = gameObject;
        
        _pool = new Pool<T>(Factory, TurnOff(), TurnOn(), AmountStart);
        
        Debug.Log(prefab);
    }

    private T Factory()
    {
        actualprefab = Object.Instantiate( prefab as GameObject);
        
        Debug.Log(actualprefab);
         
        return actualprefab as T;
    }

    public T AcquireObj()
    {
        return actualprefab as T;
    }
    
    public Action<T> TurnOn()
    {
        return default;
    }

    public Action<T> TurnOff()
    {
        return default;
    }
    
    public T ReturnObj()
    {
        return default;
    }
}

