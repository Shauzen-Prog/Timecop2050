using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private PowerUpConfiguration _powerUpConfiguration;
    private PowerUpFactory _powerUpFactory;

    public static PowerUpSpawner instance;

    public Transform[] spawnPoints;

    public GameObject speedUp;
    public GameObject damageUp;


    private int random;
    public List<GameObject> powerUps;
    int xPos;
    int yPos;
    public int powerUpCount;
    
    public int extrapowerupCount;
    private void Awake()
    {
        _powerUpFactory = new PowerUpFactory(Instantiate(_powerUpConfiguration));
    }

    private void Start()
    {
        StartCoroutine(PowerUpDrop());

        var speedUpPowerUp = _powerUpFactory.Create("BulletSpeedUp");
        
        speedUpPowerUp.transform.position += transform.up * 2 * Time.deltaTime;

    }


    private IEnumerator PowerUpDrop()
    {
        if(JsonManager.instance.data.startingPowerUp == "ShootSpeedUp")
        {
            Debug.Log("toy en speed");
            while (extrapowerupCount < 10)
            {
                Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Instantiate(speedUp, _sp.position, _sp.rotation);
                yield return new WaitForSeconds(1f);
                extrapowerupCount++;
            }
        }

        if(JsonManager.instance.data.startingPowerUp == "DamageUp")
        {
            Debug.Log("toy en damage up");
            while (extrapowerupCount < 10)
            {
                Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

                Instantiate(damageUp, _sp.position, _sp.rotation);
                yield return new WaitForSeconds(1f);
                extrapowerupCount++;
            }
        }

        while (powerUpCount < 20)
        {
            random = 0;
            random = Random.Range(0, powerUps.Count);
            Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

            Instantiate(powerUps[random], _sp.position, _sp.rotation);
            yield return new WaitForSeconds(10f);
            powerUpCount++;
        }
    }
}
