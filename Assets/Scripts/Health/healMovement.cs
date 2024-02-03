using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healMovement : MonoBehaviour
{
    public GameObject HealObject;
    float xPos;
    float yPos;
    private int _healcount;
    public int amountInTotal;
    public float timeBetweenDrop;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("sdasdad");
        StartCoroutine(HealDrop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator HealDrop()
    {
        while (_healcount < 10)
        {
            xPos = Random.Range(3f, 27f);
            yPos = 50f;
            Instantiate(HealObject, new Vector3(xPos, yPos, -40f), Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenDrop);
            _healcount++;
        }
    }
}
