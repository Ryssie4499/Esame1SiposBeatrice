using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public GameObject BombGem;
    public GameObject XP;
    public GameObject EnemyWeapon;
    public int maxHP = 30;
    public int health;
    public int movementSpeed;
    public int bulletDamage = 10;
    public int bombDamage = 30;
    [SerializeField] float spawnRate;
    int chance;
    float spawnTimer;
    bool up, down;
    PlayerMovement pM;
    GameManager GM;
    EnemySpawner eS;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        pM = FindObjectOfType<PlayerMovement>();
        eS = FindObjectOfType<EnemySpawner>();
        health = maxHP;
    }

    void Update()
    {
        if (gameObject.transform.position.y <= 6 && gameObject.transform.position.y >= 0)       //se è compreso tra 0 e 5 va giù
        {
            up = false;
            down = true;
            if(gameObject.transform.position.y >0 && gameObject.transform.position.y < 0.01f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, -2f, gameObject.transform.position.z);
                up = false;
                down = true;
            }
            
            
        }
        else if (gameObject.transform.position.y >= -6 && gameObject.transform.position.y < 0)  //se è compreso tra 0 e -5 va su
        {
            up = true;
            down = false;
            if (gameObject.transform.position.y < 0 && gameObject.transform.position.y > -0.01f)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 2f, gameObject.transform.position.z);
                up = true;
                down = false;
            }
            
            //if (gameObject.transform.position.y == 0)
            //{
            //    up = false;
            //    down = true;
            //}
            //Debug.Log("Up: " + up);
            //Debug.Log("Down: " + down);
        }
        
       
        if (GM.gameStatus == GameManager.GameStatus.gameRunning && pM.l3 == false)
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            EnemyBullet();
        }
        else if (GM.gameStatus == GameManager.GameStatus.gameRunning && pM.l3 == true)
        {
            if (down == true && up == false)
            {
                transform.Translate(new Vector3(-1, -1, 0) * movementSpeed * Time.deltaTime);
                EnemyBullet();
            }
            else if (up == true && down == false)
            {
                transform.Translate(new Vector3(-1, 1, 0) * movementSpeed * Time.deltaTime);
                EnemyBullet();
            }

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
                Instantiate(XP, transform.position, Quaternion.identity);

                chance = Random.Range(0, 4);
                if (chance == 3)
                {
                    Instantiate(BombGem, transform.position, Quaternion.identity);
                }

                else if (chance == 0 && pM.l2 == false && pM.l3 == false)
                {
                    Instantiate(EnemyWeapon, transform.position, Quaternion.identity);
                }
                EnemyDefeat();
            }
        }
        if (coll.collider.CompareTag("Bomb"))
        {
            if (health > 0)
                health -= bombDamage;
            if (health <= 0)
            {
                Instantiate(XP, transform.position, Quaternion.identity);
                EnemyDefeat();
            }
        }
    }
    public void EnemyDefeat()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void EnemyBullet()
    {
        if (pM.l2 == true && pM.l3 == false)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                Instantiate(EnemyWeapon, new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z), Quaternion.identity);
                spawnTimer = 0;
            }
        }
        if (pM.l3 == true)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                Instantiate(EnemyWeapon, new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z), Quaternion.identity);
                spawnTimer = 0;
            }
        }
    }

}
