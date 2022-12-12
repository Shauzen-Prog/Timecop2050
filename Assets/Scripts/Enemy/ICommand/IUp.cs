using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IUp : ICommand
{
    Transform _t;
    float _speed;

    public IUp(Transform transform, float speed)
    {
        _t = transform;
        _speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Do()
    {
        _t.position += _t.up * _speed  * Time.deltaTime;
    }
}
