using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> 
{
    Func<T> Factory;
    Action<T> Disable;
    Action<T> Active;

    List<T> listObj = new List<T>();

    public Pool(Func<T> factory, Action<T> disableFunction, Action<T> activeFunction,int initial = 5)
    {
        Factory = factory;
        Disable = disableFunction;
        Active = activeFunction;

        for (int i = 0; i < initial; i++)
        {
            var obj = Factory();
            Disable(obj);
            listObj.Add(obj);
        }
    }
    
    public T AcquireObj()
    {
        if (listObj.Count <= 0)
            return Factory();

        var obj = listObj[0];
        listObj.RemoveAt(0);
        Active(obj);
        return obj;
    }

    public void ReturnObj(T obj)
    {
        Disable(obj);
        listObj.Add(obj);
    }
}
