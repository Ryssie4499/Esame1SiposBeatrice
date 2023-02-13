using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] Transform upperSpawnLimit;
    [SerializeField] Transform lowerSpawnLimit;
    [SerializeField] int spawnRate;

    PlayerMovement pM;

    public int numOfEnemies;
    float spawnTimer;
    float upperLimit, lowerLimit;

    private void Start()
    {
        pM = FindObjectOfType<PlayerMovement>();
        upperLimit = upperSpawnLimit.position.y;
        lowerLimit = lowerSpawnLimit.position.y;
    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(upperLimit, lowerLimit), -2);
            if (pM.levelCount == 0)
            {
                if (numOfEnemies < 10)
                {
                    GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                    spawnTimer = 0;
                    numOfEnemies++;
                }
            }
            if (pM.levelCount == 1)
            {
                if (numOfEnemies < 3)
                {
                    GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                    spawnTimer = 0;
                    numOfEnemies++;
                }
            }
        }
    }
}
