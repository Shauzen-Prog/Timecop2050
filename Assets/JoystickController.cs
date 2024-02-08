using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public FixedJoystick _joystick;

    public float moveSpeed;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * moveSpeed, _joystick.Vertical * moveSpeed, _rigidbody.velocity.z);
        
        
    }
}
