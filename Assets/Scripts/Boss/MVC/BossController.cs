using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour,IObserver
{
    private BossModel _bossModel;
    public BulletHellWeapon bulletHellWeapon;
    
    public Transform[] waypoints;

    public float speed;
    private int _curWaypoint;
    private readonly bool _patrol = true;
    private Vector3 _target;
    private Vector3 _moveDirection;
    private Vector3 _velocity;

    private Action _artificialUpdate;
    private const bool NoAbleToShoot = false;
    
    private IObservable _myModel;
    
    private void Start()
    {
        _myModel = GetComponent<Entity>();
        _bossModel = GetComponent<BossModel>();
        _myModel.Subscribe(this);
        _artificialUpdate = MoveBoss;
    }

    public void Update()
    {
        _artificialUpdate();
    }

    private void MoveBoss()
    {
        if (_curWaypoint < waypoints.Length)
        {
            _target = waypoints[_curWaypoint].position;
            _moveDirection = _target - _bossModel.transform.position;
            _velocity = _bossModel.rigidbody.velocity;
            
            if (_moveDirection.magnitude < 1)
            {
                _curWaypoint = Random.Range(0, waypoints.Length);
            }
            else
            {
                _velocity = _moveDirection.normalized * speed;
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
        _bossModel.rigidbody.velocity = _velocity;
    }

    public void Notify(EventEnum eventEnum, params object[] parameters)
    {
        if (eventEnum == EventEnum.Death)
        {
            bulletHellWeapon.canShoot = NoAbleToShoot;
        }
    }

    private void OnDisable()
    {
        _myModel.UnSubscribe(this);
    }
}


