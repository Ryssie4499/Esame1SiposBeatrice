using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("SpawnPosition")]
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] Transform upperSpawnLimit;
    [SerializeField] Transform lowerSpawnLimit;

    [Header("SpawnRate")]
    [SerializeField] float spawnRate;
    public int numOfEnemies;

    //references
    PlayerMovement pM;
    GameManager GM;

    //limiti di tempo e di spazio dello spawn
    float spawnTimer;
    float upperLimit, lowerLimit;
    [HideInInspector] public Vector3 spawnPosition;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        pM = FindObjectOfType<PlayerMovement>();

        //la posizione dei due limiti è data dalla posizione in scena dei due oggetti UpperLimit e LowerLimit (in Enemies)
        upperLimit = upperSpawnLimit.position.y;
        lowerLimit = lowerSpawnLimit.position.y;
    }

    //solo se il gioco è in play, gli enemy spawneranno in quantità diverse in base al livello
    private void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnPosition = new Vector3(transform.position.x, Random.Range(upperLimit, lowerLimit), -2);

                //livello 1
                if (pM.l2 == false)
                {
                    if (numOfEnemies < 10)
                    {
                        GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                        spawnTimer = 0;
                        numOfEnemies++;
                    }
                }

                //livello 2
                if (pM.l2 == true && pM.l3 == false)
                {
                    if (numOfEnemies < 3)
                    {
                        GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                        spawnTimer = 0;
                        numOfEnemies++;
                    }
                }

                //livello 3
                if (pM.l3 == true)
                {
                    if (numOfEnemies < 4)
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
