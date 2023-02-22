using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject BossWeapon;
    public int maxHP = 30;
    public int health;
    public int bulletDamage = 10;
    public int bombDamage = 30;
    [SerializeField] float spawnRate, otherSpawnRate, rareSpawnRate, r2SpawnRate;
    float spawnTimer, otherSpawnTimer, rareSpawnTimer, r2SpawnTimer;
    GameManager GM;
    UIManager UM;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        UM = FindObjectOfType<UIManager>();
        health = maxHP;
    }
    private void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            EnemyBullet();
        }
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {
            if (health > 0)
                health -= bulletDamage;
            if (health <= 0)
            {
                BossDefeat();
            }
        }
        if (coll.collider.CompareTag("Bomb"))
        {
            if (health > 0)
                health -= bombDamage;
            if (health <= 0)
            {
                BossDefeat();
            }
        }
    }
    public void BossDefeat()
    {
        if (health == 0)
        {
            GM.gameStatus = GameManager.GameStatus.gameEnd;
            Destroy(gameObject);
            UM.endCanvas.SetActive(true);
        }
    }
    public void EnemyBullet()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            Instantiate(BossWeapon, new Vector3(transform.position.x - 12f, transform.position.y, transform.position.z), Quaternion.identity);
            spawnTimer = 0;
        }
        otherSpawnTimer += Time.deltaTime;
        if(otherSpawnTimer>= otherSpawnRate)
        {
            Instantiate(BossWeapon, new Vector3(transform.position.x - 12f, transform.position.y + 4f, transform.position.z), Quaternion.identity);
            Instantiate(BossWeapon, new Vector3(transform.position.x - 12f, transform.position.y - 4f, transform.position.z), Quaternion.identity);
            otherSpawnTimer = 0;
        }
        rareSpawnTimer += Time.deltaTime;
        if (rareSpawnTimer >= rareSpawnRate)
        {
            Instantiate(BossWeapon, new Vector3(transform.position.x - 12f, transform.position.y + 2f, transform.position.z), Quaternion.identity);
            rareSpawnTimer = 0;
        }
        r2SpawnTimer += Time.deltaTime;
        if (r2SpawnTimer >= r2SpawnRate)
        {
            Instantiate(BossWeapon, new Vector3(transform.position.x - 12f, transform.position.y - 2f, transform.position.z), Quaternion.identity);
            r2SpawnTimer = 0;
        }
       
    }
}