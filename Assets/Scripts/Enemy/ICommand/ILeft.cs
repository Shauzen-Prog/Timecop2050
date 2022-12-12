using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILeft : ICommand
{
    Transform _t;
    float _speed;

    public ILeft(Transform transform, float speed)
    {
        _t = transform;
        _speed = speed;
    }

    public void Do()
    {
        _t.position -= _t.right * _speed  * Time.deltaTime;
    }
}
