using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFingerMove
{
    Transform _playerTransform;
    private float dist;
    public static bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;

    float maxClampX = 11.8f;
    float minClampX = -11.8f;

    float maxClampY = 22.62f;
    float minClampY = -20f;

    public DragFingerMove(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    Vector3 ClampPositions()
    {
        float clampPositionX = Mathf.Clamp(_playerTransform.position.x, minClampX, maxClampX);
        float clampPositionY = Mathf.Clamp(_playerTransform.position.y, minClampY, maxClampY);
        Vector3 clampPositionVector = new Vector3(clampPositionX, clampPositionY, 0f);
        return clampPositionVector;
    }

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
}

