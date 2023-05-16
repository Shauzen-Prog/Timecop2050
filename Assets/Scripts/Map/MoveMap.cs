using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    [SerializeField] private float _speed = 100;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.forward * (_speed * Time.deltaTime);
    }
}
