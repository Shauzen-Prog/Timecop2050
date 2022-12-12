using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healMovement : MonoBehaviour
{
    public GameObject HealObject;
    int xPos;
    int yPos;
    public int Healcount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HealDrop());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator HealDrop()
    {
        while (Healcount < 10)
        {
            xPos = Random.Range(-13, 14);
            yPos = 28;
            Instantiate(HealObject, new Vector3(xPos, yPos, 0f), Quaternion.identity);
            yield return new WaitForSeconds(15f);
            Healcount++;
        }
    }
}
