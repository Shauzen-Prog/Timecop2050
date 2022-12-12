using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemySpawner : MonoBehaviour
{
    public GameObject enemyChase;
    int xPos;
    int yPos;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EnemyDrop()
    {
        while(enemyCount < 20)
        {
            xPos = Random.Range(16,-17);
            yPos = Random.Range(0,26);
            Instantiate(enemyChase, new Vector3(xPos, 29, 0f), Quaternion.identity);
            yield return new WaitForSeconds(10f);
            enemyCount++; 
        }
    }
}
