using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRight : ICommand
{
    Transform _t;
    float _speed;

    public IRight(Transform transform, float speed)
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
        _t.position += _t.right * _speed * Time.deltaTime;
    }
}
