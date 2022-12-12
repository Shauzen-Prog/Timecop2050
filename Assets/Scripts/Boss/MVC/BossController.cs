using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController
{
    private Transform _myTransform;
    private Transform[] _waypoints;
    private Rigidbody _rb;
    float _speed;
    int _curWaypoint;
    bool _patrol = true;
    private Vector3 _target;
    Vector3 _moveDirection;
    Vector3 _velocity;

    public BossController(Transform myTransform ,Transform[] waypoints, Rigidbody rb, float speed,
        int curWaypoint, bool patrol, Vector3 moveDirection, Vector3 velocity)
    {
        _myTransform = myTransform;
        _waypoints = waypoints;
        _rb = rb;
        _speed = speed;
        _curWaypoint = curWaypoint;
        _patrol = patrol;
        _moveDirection = moveDirection;
        _velocity = velocity;
    }

    public void UpdateController()
    {
        if (_curWaypoint < _waypoints.Length)
        {
            _target = _waypoints[_curWaypoint].position;
            _moveDirection = _target - _myTransform.position;
            _velocity = _rb.velocity;


            if (_moveDirection.magnitude < 1)
            {
                var beforeWaypoint = _curWaypoint;
                _curWaypoint = Random.Range(0, _waypoints.Length);
                if(beforeWaypoint < _curWaypoint)
                {
                    //se mueve a la izquierda 
                }
                if (beforeWaypoint > _curWaypoint)
                {
                    //se mueve a la der
                }
            }
            else
            {
                _velocity = _moveDirection.normalized * _speed;
            }
        }
        else
        {
            if (_patrol)
            {
                _curWaypoint = 0;
            }
            else
            {
                _velocity = Vector3.zero;
            }
        }
        _rb.velocity = _velocity;
    }
}


