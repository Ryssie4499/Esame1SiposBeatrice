using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float spawnRate;
    public GameObject BombGem;
    public GameObject XP;
    public GameObject EnemyWeapon;

    public int maxHP;
    public int health;
    public int movementSpeed;
    public int bulletDamage;
    public int bombDamage;

    [HideInInspector] public int chance;
    [HideInInspector] public float spawnTimer;
    [HideInInspector] public bool up, down;

    [HideInInspector] public PlayerMovement pM;
    [HideInInspector] public GameManager GM;
    [HideInInspector] public Rigidbody rb;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        pM = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        health = maxHP;
    }

    public virtual void Movement()
    {

    }
    public virtual void Shoot()
    {

    }
    public virtual void EnemyDefeat()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void OnCollisionEnter(Collision coll)
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

}
