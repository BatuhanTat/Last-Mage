using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float startWait = 1.0f;
    [SerializeField] float waveInterval = 4.0f;
    [SerializeField] float spawnInterval = 0.5f;
    [SerializeField] int enemiesPerWave = 5;

    [Space(2)]
    [Header("Difficulty Variables")]
    [SerializeField] float waveInterval_Decrease = 0.5f;
    [SerializeField] int enemiesPerWave_Increase = 2;
    [Tooltip("Increase difficulty every 'x' seconds")]
    [SerializeField] float difficultyIncreaseInterval = 30.0f;
    [SerializeField] float enemyTypeHandlerInterval = 60.0f;

    [Space(2)]
    [Header("Enemy Spawn Position")]
    [SerializeField] GameObject player;
    [Tooltip("Determine how far away enemies will spawn from player")]
    [SerializeField] float radius = 10.0f;

    private Coroutine spawnRoutine;
    bool enemyRandom = false;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 3;
        // PrintSpawnVariables();
        spawnRoutine = StartCoroutine(SpawnEnemyWaves());
        InvokeRepeating("MakeGameHarder",0,difficultyIncreaseInterval);
        InvokeRepeating("EnemyTypeHandler",enemyTypeHandlerInterval,enemyTypeHandlerInterval);
    }

    Vector2 RandomPosition()
    {
        //Vector2 randomPosition = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width),
        //                                                                    Random.Range(0, Screen.height)));
        /*     Vector2 randomPosition = Random.insideUnitCircle.normalized;
            randomPosition *= Random.Range(10, 12); */

        Vector2 randomPosition = Random.insideUnitCircle.normalized * radius;
        randomPosition = (Vector2)player.transform.position + randomPosition;
        return randomPosition;
    }

    // Make game harder every 'x', 30.0, seconds.
    void MakeGameHarder()
    {
        // waveInterval will be capped at 1.0f. It will not go lower.
        if (waveInterval >= 1.5f)
        {
            waveInterval -= waveInterval_Decrease;
        }
        enemiesPerWave += enemiesPerWave_Increase;
        // PrintSpawnVariables();
    }
    void EnemyTypeHandler()
    {   if(enemyTypeLimit < 3)
        {
            enemyTypeLimit++;
        }
        else if (enemyTypeLimit == 3)
        {
            enemyRandom = true;
        }
        //enemyTypeLimit = (enemyTypeLimit < 4) ? enemyTypeLimit + 1 : enemyTypeLimit; 
    }

    void PrintSpawnVariables()
    {
        Debug.Log("waveInterval : " + waveInterval);
        Debug.Log("enemiesPerWave : " + enemiesPerWave);
    }

    int enemyTypeLimit = 0;
    IEnumerator SpawnEnemyWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {   
            int waveType;
            if(enemyRandom)
            {
                waveType = Random.Range(0, enemyTypeLimit);
            }
            else
            {
                waveType = enemyTypeLimit;
            }    
            for (int i = 0; i < enemiesPerWave; i++)
            {
                Vector2 randomPosition = RandomPosition();
                if (waveType == 0)
                {
                    GameObject enemy0 = ObjectPool.SharedInstance.GetPooledObject("Enemy0");
                    if (enemy0 != null)
                    {
                        enemy0.transform.position = randomPosition;
                        enemy0.transform.rotation = Quaternion.identity;
                        //enemy0.SetActive(true);
                        enemy0.GetComponent<Enemy>().ReactivateEnemy();
                    }
                }
                else if (waveType == 1)
                {
                    GameObject enemy1 = ObjectPool.SharedInstance.GetPooledObject("Enemy1");
                    if (enemy1 != null)
                    {
                        enemy1.transform.position = randomPosition;
                        enemy1.transform.rotation = Quaternion.identity;
                        //enemy1.SetActive(true);
                        enemy1.GetComponent<Enemy>().ReactivateEnemy();
                    }
                }
                else if (waveType == 2)
                {
                    GameObject enemy2 = ObjectPool.SharedInstance.GetPooledObject("Enemy2");
                    if (enemy2 != null)
                    {
                        enemy2.transform.position = randomPosition;
                        enemy2.transform.rotation = Quaternion.identity;
                        //enemy2.SetActive(true);
                        enemy2.GetComponent<Enemy>().ReactivateEnemy();
                    }
                }
                else if (waveType == 3)
                {
                    GameObject enemy3 = ObjectPool.SharedInstance.GetPooledObject("Enemy3");
                    if (enemy3 != null)
                    {
                        enemy3.transform.position = randomPosition;
                        enemy3.transform.rotation = Quaternion.identity;
                        //enemy3.SetActive(true);
                        enemy3.GetComponent<Enemy>().ReactivateEnemy();
                    }
                }
                yield return new WaitForSeconds(spawnInterval);
            }
            yield return new WaitForSeconds(waveInterval);

            if (GameManager.instance.isGameActive == false)
            {
                StopCoroutine(spawnRoutine);
            }
        }
    }
}
