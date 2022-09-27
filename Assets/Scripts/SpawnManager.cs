using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float startWait = 1.0f;
    [SerializeField] float waveInterval = 4.0f;
    [SerializeField] float spawnInterval = 0.5f;
    [SerializeField] int enemiesPerWave = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    Vector2 RandomPosition_InsideCameraView()
    {
        Vector2 randomPosition = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width),
                                                                            Random.Range(0, Screen.height)));
        return randomPosition;
    }

    IEnumerator SpawnEnemyWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            //int waveType = Random.Range(0, 3);
            int waveType = 0;
            for (int i = 0; i < enemiesPerWave; i++)
            {
                Vector2 randomPosition = RandomPosition_InsideCameraView();
                if (waveType == 0)
                {
                    GameObject enemy0 = ObjectPool.SharedInstance.GetPooledObject("Enemy0");
                    if (enemy0 != null)
                    {
                        enemy0.transform.position = randomPosition;
                        enemy0.transform.rotation = Quaternion.identity;
                        enemy0.SetActive(true);
                    }
                }
                else if (waveType == 1)
                {
                    GameObject enemy1 = ObjectPool.SharedInstance.GetPooledObject("Enemy1");
                    if (enemy1 != null)
                    {
                        enemy1.transform.position = randomPosition;
                        enemy1.transform.rotation = Quaternion.identity;
                        enemy1.SetActive(true);
                    }
                }
                else if (waveType == 2)
                {
                    GameObject enemy2 = ObjectPool.SharedInstance.GetPooledObject("Enemy2");
                    if (enemy2 != null)
                    {
                        enemy2.transform.position = randomPosition;
                        enemy2.transform.rotation = Quaternion.identity;
                        enemy2.SetActive(true);
                    }
                }
                yield return new WaitForSeconds(spawnInterval);
            }
            yield return new WaitForSeconds(waveInterval);
        }
    }
}
