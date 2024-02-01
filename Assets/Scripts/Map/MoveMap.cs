using UnityEngine;

public class MoveMap : MonoBehaviour
{
    [SerializeField] private float _speed = 100;

    public Vector3 placeToMoveMap;
    public Vector3 outOfMap;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.forward * (_speed * Time.deltaTime);

        if (transform.position.y <= outOfMap.y)
        {
            transform.position = placeToMoveMap;
        }
    }
}
