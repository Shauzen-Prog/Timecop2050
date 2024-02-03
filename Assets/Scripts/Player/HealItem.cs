using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    public float speed;
    public float healAmount;
    public float timeToDestroy;

    void Update()
    {
        transform.position += transform.up * (-1 * (speed * Time.deltaTime));
        StartCoroutine(DestroyItem());
    }
    private void OnTriggerEnter(Collider other)
    {
        Entity player = other.GetComponent<Entity>();

        if (player != null && other.gameObject.layer == 8)
        {
            player.Heal(healAmount);
            Destroy(this.gameObject);
        }
        if(other.gameObject.layer == 13)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyItem()
    {
        yield return new WaitForSeconds(timeToDestroy);
        
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
