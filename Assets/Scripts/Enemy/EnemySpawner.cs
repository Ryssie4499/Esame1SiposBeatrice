using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] Transform upperSpawnLimit;
    [SerializeField] Transform lowerSpawnLimit;
    [SerializeField] float spawnRate;

    PlayerMovement pM;
    GameManager GM;

    public int numOfEnemies;
    float spawnTimer;
    float upperLimit, lowerLimit;
    [HideInInspector]public Vector3 spawnPosition;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        pM = FindObjectOfType<PlayerMovement>();
        upperLimit = upperSpawnLimit.position.y;
        lowerLimit = lowerSpawnLimit.position.y;
    }
    private void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnPosition = new Vector3(transform.position.x, Random.Range(upperLimit, lowerLimit), -2);
                if (pM.l2==false && pM.l3==false)
                {
                    if (numOfEnemies < 10)
                    {
                        GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                        spawnTimer = 0;
                        numOfEnemies++;
                    }
                }
                if (pM.l2==true && pM.l3==false)
                {
                    if (numOfEnemies < 3)
                    {
                        GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                        spawnTimer = 0;
                        numOfEnemies++;
                    }
                }
                if (pM.l3 == true)
                {
                    if (numOfEnemies < 2)
                    {
                        GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                        spawnTimer = 0;
                        numOfEnemies++;
                    }
                }
            }
        }
    }
}
