using UnityEngine;

public class MoveMap : MonoBehaviour
{
    [SerializeField] private float _speed = 100;

    public Vector3 placeToMoveMap = new Vector3(0, 0, 271.82019f);
    public Vector3 outOfMap = new Vector3(0, 0, 0);
    
    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.forward * (_speed * Time.deltaTime);

        if (transform.position.z <= 0)
        {
            transform.position = placeToMoveMap;
        }
    }
}
