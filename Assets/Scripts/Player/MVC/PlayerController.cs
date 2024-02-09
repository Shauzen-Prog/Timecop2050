using System;
using UnityEngine;

public enum TypeOfController
{
    DragMove,
    Joystick,
    NoControls
}

public class PlayerController
{
    private Transform _playerTransform;
    private readonly Rigidbody _rigidbody;
    private readonly FixedJoystick _fixedJoystick;
    private DragFingerMove _dragFingerMove;

    private Action _ArtificialUpdate;
    
    [Header("Drag And Move Variables")]
    private float dist;
    public static bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;

    float _minClampX; 
    float _maxClampX; 

    float _minClampY; 
    float _maxClampY;
    private float _moveSpeed;
    private float _normalMoveSpeed;
    private bool isUsingJoystick;

    public PlayerController(Transform playerTransform, Rigidbody rigidbody, FixedJoystick fixedJoystick,float minClampX, float maxClampX, float minClampY, float maxClampY, 
        float moveSpeed)
    {
        _playerTransform = playerTransform;
        _rigidbody = rigidbody;
        _fixedJoystick = fixedJoystick;
        _minClampX = minClampX;
        _maxClampX = maxClampX;
        _minClampY = minClampY;
        _maxClampY = maxClampY;
        _moveSpeed = moveSpeed;
    }

    
    public void OnAwake()
    {
        
        EventManager.Suscribe("ChangeController", ChangeController);
        _normalMoveSpeed = _moveSpeed;
        
#if UNITY_EDITOR
        
        _ArtificialUpdate = MoveWASDController;
        
#elif UNITY_ANDROID
        //_ArtificialUpdate = DragMove;
        ChangeController(TypeOfController.DragMove);
#endif
   
        
        
    }
    
    Vector3 ClampPositions()
    {
        float clampPositionX = Mathf.Clamp(_playerTransform.position.x, _minClampX, _maxClampX);
        float clampPositionY = Mathf.Clamp(_playerTransform.position.y, _minClampY, _maxClampY);
        Vector3 clampPositionVector = new Vector3(clampPositionX, clampPositionY, -40f);
        return clampPositionVector;
    }

    
    public void OnUpdate()
    {
        _ArtificialUpdate();
        ClampPositions();
    }

    public void ChangeController(params object[] parameters)
    {
        var typeOfController = (TypeOfController)parameters[0];
        
        switch(typeOfController)
        {
            case TypeOfController.DragMove:
                isUsingJoystick = false;
                _ArtificialUpdate = DragMove;
                break;

            case TypeOfController.Joystick:
                _ArtificialUpdate = () => { };
                isUsingJoystick = true;
                break;
            case TypeOfController.NoControls:
                _ArtificialUpdate = () => { };
                isUsingJoystick = false;
                break;
        }
    }

    public void OnFixedUpdate()
    {
        if(!isUsingJoystick) return;
        
        _rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * _moveSpeed, _fixedJoystick.Vertical * _moveSpeed, _rigidbody.velocity.z);
    }
    
    #if UNITY_EDITOR
    
    private void MoveWASDController()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
        var _moveVector = new Vector3(horizontal, vertical, 0);
        
        _playerTransform.position += _moveVector * (20 * Time.deltaTime);
        
    }
        
    #endif
    
  

    public void DragMove()
    {
        _playerTransform.localPosition = ClampPositions();

        Vector3 v3;

        // Can use Input.touchCount == 0 for slow time with Time.ScaleTime

        if (Input.touchCount != 1)
        {
            //Time.timeScale = 0.3f; De esta manera funciona
            dragging = false;
            return;
        }

        //Time.timeScale = 1f; implementar con eventos

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    toDrag = hit.transform;
                    dist = hit.transform.position.z - Camera.main.transform.position.z;
                    v3 = new Vector3(pos.x, pos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    offset = toDrag.position - v3;
                    dragging = true;
                }
            }
        }

        if (dragging && touch.phase == TouchPhase.Moved)
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            toDrag.position = v3 + offset;
        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;
        }
    }

    public void OnDisable()
    {
        EventManager.UnSuscribe("ChangeController", ChangeController);
    }
}
