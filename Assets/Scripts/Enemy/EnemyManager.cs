using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    [Header("Bullets & Collectible Spawn")]
    [SerializeField] float spawnRate;
    public GameObject BombGem;
    public GameObject XP;
    public GameObject EnemyWeapon;

    [Header("Stats")]
    public int maxHP = 30;
    public int health;
    public int movementSpeed;
    public int bulletDamage = 10;
    public int bombDamage = 30;

    //bullet e collectible
    int chance;
    float spawnTimer;
    bool up, down;

    //references
    PlayerMovement pM;
    GameManager GM;
    Rigidbody rb;

    //la vita iniziale dell'enemy è massima
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        pM = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        health = maxHP;
    }

    void Update()
    {
        //se il gioco è in play nel livello 1 o nel livello 2, gli enemy si muovono verso sinistra e spawnano i proiettili
        if (GM.gameStatus == GameManager.GameStatus.gameRunning && pM.l3 == false)
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            EnemyBullet();
        }

        //se il gioco è in play nel livello 3, gli enemy si muovono a zigzag verso sinistra
        else if (GM.gameStatus == GameManager.GameStatus.gameRunning && pM.l3 == true)
        {
            //se l'enemy si trova tra il boundary superiore e il centro, si muoverà verso il basso
            if (gameObject.transform.position.y <= 7 && gameObject.transform.position.y >= 0)
            {
                up = false;
                down = true;

                //quando arriva vicino al centro, una forza lo spinge verso la parte inferiore della scena e continuerà a muoversi verso il basso fino al boundary inferiore
                if (gameObject.transform.position.y > 0 && gameObject.transform.position.y < 0.01f)
                {
                    rb.AddForce(Vector3.down * 3f, ForceMode.Impulse);
                    up = false;
                    down = true;
                }
            }

            //se l'enemy si trova tra il boundary inferiore e il centro, si muoverà verso l'alto
            else if (gameObject.transform.position.y >= -7 && gameObject.transform.position.y < 0)
            {
                up = true;
                down = false;

                //quando arriva vicino al centro, una forza lo spinge verso la parte superiore della scena e continuerà a muoversi verso l'alto fino al boundary superiore
                if (gameObject.transform.position.y < 0 && gameObject.transform.position.y > -0.01f)
                {
                    rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
                    up = true;
                    down = false;
                }
            }

            //movimento verso il basso e spawn dei proiettili
            if (down == true && up == false)
            {
                transform.Translate(new Vector3(-1, -1, 0) * movementSpeed * Time.deltaTime);
                EnemyBullet();
            }

            //movimento verso l'alto e spawn dei proiettili
            else if (up == true && down == false)
            {
                transform.Translate(new Vector3(-1, 1, 0) * movementSpeed * Time.deltaTime);
                EnemyBullet();
            }
        }
    }

    //quando l'enemy viene colpito da un proiettile o da una bomba, perde vita e se muore, spawna randomicamente dei collectible o delle trappole
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {
            if (health > 0)
            {
                health -= bulletDamage;
            }

            //alla morte dell'enemy, spawnano i punti esperienza e se si trova al livello 1, può spawnare randomicamente trappole o collectibles, sennò solo collectibles (ma non sempre)
            if (health <= 0)
            {
                Instantiate(XP, transform.position, Quaternion.identity);

                chance = Random.Range(0, 4);

                if (chance == 3)
                {
                    Instantiate(BombGem, transform.position, Quaternion.identity);
                }

                //livello 1
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
            {
                health -= bombDamage;
            }

            //alla morte dell'enemy, spawnano solo i punti esperienza (perchè con la bomba è più facile uccidere un nemico)
            if (health <= 0)
            {
                Instantiate(XP, transform.position, Quaternion.identity);
                EnemyDefeat();
            }
        }
    }

    //morte dell'enemy
    public void EnemyDefeat()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //gli enemy del secondo livello spawnano i proiettili più lontani perchè hanno una shape più grande
    public void EnemyBullet()
    {
        //livello 2
        if (pM.l2 == true && pM.l3 == false)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                Instantiate(EnemyWeapon, new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z), Quaternion.identity);
                spawnTimer = 0;
            }
        }

        //livello 3
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
