using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject BossWeapon;

    [Header("Stats")]
    public int maxHP = 30;
    public int health;
    public int bulletDamage = 10;                                                   //danno inflitto al Boss con un semplice Bullet
    public int bombDamage = 30;                                                     //danno inflitto al Boss con una Bomb

    [Header("Bullets' rate")]
    [SerializeField] float spawnRate, otherSpawnRate, rareSpawnRate, r2SpawnRate;   //i proiettili del boss spawnano a 4 rate diversi e con 4 timer diversi che si resettano
    float spawnTimer, otherSpawnTimer, rareSpawnTimer, r2SpawnTimer;

    //references
    GameManager GM;
    UIManager UM;

    //la vita iniziale del Boss è uguale alla vita massima che il Boss può avere
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        UM = FindObjectOfType<UIManager>();

        health = maxHP;
    }
    private void Update()
    {
        //solo se il gioco è in play spawnano i proiettili
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            BossBullet();
        }
    }

    //ogni volta che il boss viene colpito da bombe o proiettili la sua vita diminuisce e se = 0, viene sconfitto
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {
            if (health > 0)
            {
                health -= bulletDamage;
            }

            if (health <= 0)
            {
                BossDefeat();
            }
        }
        if (coll.collider.CompareTag("Bomb"))
        {
            if (health > 0)
            {
                health -= bombDamage;
            }

            if (health <= 0)
            {
                BossDefeat();
            }
        }
    }

    //sconfitta del Boss: finisce il gioco, il Boss viene distrutto e si attivano gli EndCanvas
    public void BossDefeat()
    {
        if (health == 0)
        {
            GM.gameStatus = GameManager.GameStatus.gameEnd;
            Destroy(gameObject);
            UM.endCanvas.SetActive(true);
        }
    }

    //il Boss spara sempre lo stesso tipo di proiettile, ma con quattro rate diversi
    public void BossBullet()
    {
        //questi proiettili viaggiano al centro
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            Instantiate(BossWeapon, new Vector3(transform.position.x - 12f, transform.position.y, transform.position.z), Quaternion.identity);
            spawnTimer = 0;
        }

        //questi proiettili viaggiano sopra e sotto la fila centrale
        otherSpawnTimer += Time.deltaTime;
        if (otherSpawnTimer >= otherSpawnRate)
        {
            Instantiate(BossWeapon, new Vector3(transform.position.x - 12f, transform.position.y + 4f, transform.position.z), Quaternion.identity);
            Instantiate(BossWeapon, new Vector3(transform.position.x - 12f, transform.position.y - 4f, transform.position.z), Quaternion.identity);
            otherSpawnTimer = 0;
        }

        //questi proiettili viaggiano tra la fila centrale e la fila superiore
        rareSpawnTimer += Time.deltaTime;
        if (rareSpawnTimer >= rareSpawnRate)
        {
            Instantiate(BossWeapon, new Vector3(transform.position.x - 12f, transform.position.y + 2f, transform.position.z), Quaternion.identity);
            rareSpawnTimer = 0;
        }

        //questi proiettili viaggiano tra la fila centrale e la fila inferiore
        r2SpawnTimer += Time.deltaTime;
        if (r2SpawnTimer >= r2SpawnRate)
        {
            Instantiate(BossWeapon, new Vector3(transform.position.x - 12f, transform.position.y - 2f, transform.position.z), Quaternion.identity);
            r2SpawnTimer = 0;
        }

    }
}
