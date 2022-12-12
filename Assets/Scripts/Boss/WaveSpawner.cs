using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState{ SPAWNING, WATING, COUNTING }
       
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public Transform enemy;
        public int count;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    public int levelCompleteIndex;

    private void Awake()
    {
        JsonManager.instance.Load();
    }

    private void Start()
    {
        if (spawnPoints.Length == 0) Debug.LogError("No Spawnpoint Assigned");
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        CheckAndChangeStates();

        if(Input.GetKeyDown(KeyCode.K))
        {
            JsonManager.instance.data.completeLevel[levelCompleteIndex] = true;

            Debug.Log(levelCompleteIndex);

            if(levelCompleteIndex == 3 && JsonManager.instance.data.completeLevel[levelCompleteIndex] == true)
            {
                SceneManagement.instance.LoadScene(0);
            }
            else
            {
                var sceneToLoad = SceneManagement.ReturnThisSceneIndex() + 1;

                SceneManagement.instance.LoadScene(sceneToLoad);
            }
        }
    }

    void CheckAndChangeStates()
    {
        if (state == SpawnState.WATING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                return;
            }
            else return;
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            Debug.Log("All Waves Complete! Change Level");
            var sceneToLoad = SceneManagement.ReturnThisSceneIndex() + 1;

            if(sceneToLoad == 4)
            {
                SceneManagement.instance.LoadScene(6);
            }
            else
            {
                JsonManager.instance.data.sceneToLoad = sceneToLoad;

                JsonManager.instance.data.completeLevel[levelCompleteIndex] = true;


                SceneManagement.instance.LoadScene(4);
            }
        }
        else nextWave++;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) return false;
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.WATING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
