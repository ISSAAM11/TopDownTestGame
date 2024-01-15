using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spowner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject StrongenemyPrefab;
    public GameObject SuperenemyPrefab;
    public float minSpawnTime ;
    public float maxSpawnTime ;
    public Transform PlayerTransform;

    public float maxspawnRadius;
    public float minspawnRadius;

    public float spawnTime = 1f; 


    void Start()
    {
        InvokeRepeating("SpawnEnemy", Random.Range(minSpawnTime, maxSpawnTime), Random.Range(minSpawnTime, maxSpawnTime));



        InvokeRepeating("SpawnSuperEnemy", 10, 15);
    }

    void SpawnEnemy()
    {
        Vector3 randomSpawnPosition = PlayerTransform.position + Random.insideUnitSphere * Random.Range(minspawnRadius, maxspawnRadius);
        randomSpawnPosition.y = 21.85f; 

        GameObject enemy;
        if (Random.Range(0, 10) == 0)
        {
            enemy = Instantiate(StrongenemyPrefab, randomSpawnPosition, Quaternion.identity);
            enemy.GetComponent<Enemy>().playerTransform = PlayerTransform;
        }
        else
        {
            enemy = Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
            enemy.GetComponent<Enemy>().playerTransform = PlayerTransform;
        }

    }
    void SpawnSuperEnemy()
    {
        Vector3 randomSpawnPosition = PlayerTransform.position + Random.insideUnitSphere * Random.Range(minspawnRadius, maxspawnRadius);
        randomSpawnPosition.y = 21.85f;

        Instantiate(SuperenemyPrefab, randomSpawnPosition, Quaternion.identity);
    }
}
