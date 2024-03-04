using System;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class PlayerController : MonoBehaviour
{
    private PlayerModel _playerModel;
    public FixedJoystick fixedJoystick;

    private Action _artificialUpdate;
    
    private float _dist;
    private static bool _dragging;
    private Vector3 _offset;
    private Transform _toDrag;

    [Header("Clamp To Map Size Parameters")]
    public float minClampX;
    public float maxClampX;
    public float minClampY;
    public float maxClampY;
    private const float StaticClampZ = -40f;
    
    private Camera _camera;

    [Header("Controllers Speed")] 
    public float wasdMoveSpeed = 20f;
    public float joystickMoveSpeed = 25f;
    

    private void Awake()
    {
        _playerModel = GetComponent<PlayerModel>();
        
#if UNITY_EDITOR
        
        _artificialUpdate = MoveWasdController;
        
#elif UNITY_ANDROID
        //_ArtificialUpdate = DragMove;
        ChangeController(TypeOfController.DragMove);
#endif
        
    }
    
    private void Start()
    {
        _camera = Camera.main;
    }
    
    private Vector3 ClampPositions()
    {
        var position = _playerModel.transform.position;
        var clampPositionX = Mathf.Clamp(position.x, minClampX, maxClampX);
        var clampPositionY = Mathf.Clamp(position.y, minClampY, maxClampY);
        var clampPositionVector = new Vector3(clampPositionX, clampPositionY, StaticClampZ);
        return clampPositionVector;
    }
    
    public void Update()
    {
        _playerModel.transform.localPosition = ClampPositions();
        _artificialUpdate();
    }

    private void ChangeController(params object[] parameters)
    {
        var typeOfController = (TypeOfController)parameters[0];
        
        switch(typeOfController)
        {
            case TypeOfController.DragMove:
                _artificialUpdate = DragMove;
                break;
            case TypeOfController.Joystick:
                _artificialUpdate = UseJoystick;
                break;
            case TypeOfController.NoControls:
                _artificialUpdate = () => { };
                break;
        }
    }

    private void UseJoystick()
    {
        _playerModel.rigidbody.velocity = new Vector3(fixedJoystick.Horizontal * joystickMoveSpeed,
            fixedJoystick.Vertical * joystickMoveSpeed, _playerModel.rigidbody.velocity.z);
    }
    
    #if UNITY_EDITOR
    
    private void MoveWasdController()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var moveVector = new Vector3(horizontal, vertical, 0);

        _playerModel.transform.position += moveVector * (wasdMoveSpeed * Time.deltaTime);
    }
        
    #endif

    #if UNITY_ANDROID
    private void DragMove()
    {
        _playerModel.transform.localPosition = ClampPositions();

        Vector3 v3;
        
        if (Input.touchCount != 1)
        {
            _dragging = false;
            return;
        }

        var touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            var ray = _camera.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == 8)
                {
                    _toDrag = hit.transform;
                    _dist = hit.transform.position.z - _camera.transform.position.z;
                    v3 = new Vector3(pos.x, pos.y, _dist);
                    v3 = _camera.ScreenToWorldPoint(v3);
                    _offset = _toDrag.position - v3;
                    _dragging = true;
                }
            }
        }

        if (_dragging && touch.phase == TouchPhase.Moved)
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _dist);
            v3 = _camera.ScreenToWorldPoint(v3);
            _toDrag.position = v3 + _offset;
        }

        if (_dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            _dragging = false;
        }
    }
    #endif

    public void OnDisable()
    {
        EventManager.UnSuscribe("ChangeController", ChangeController);
    }
}
