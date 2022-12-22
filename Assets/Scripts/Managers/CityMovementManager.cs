using UnityEngine;

public class CityMovementManager : MonoBehaviour
{
    [SerializeField] private Vector3 _resetPosition;

    private void OnTriggerEnter(Collider other)
    {
        var map = other.GetComponent<MoveMap>();
        
        if(map == null) return;
        
        Debug.Log("entre");
        
        map.transform.position = _resetPosition;
    }
}
