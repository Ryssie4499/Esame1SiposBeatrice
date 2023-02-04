using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public GameObject BombGem;
    public GameObject EnemyWeapon;
    public int maxHP = 30;
    public int health;
    public int movementSpeed;
    public int bulletDamage = 30;
    int probability;
    int points;
    private void Start()
    {
        health = maxHP;
    }

    void Update()
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {
            points += 20;
            if (health > 0)
                health -= bulletDamage;
            if (health <= 0)
            {
                probability = Random.Range(0, 4);
                if(probability==3)
                {
                    Instantiate(BombGem, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(EnemyWeapon, transform.position, Quaternion.identity);
                }
                EnemyDefeat();
            }
        }
    }
    public void EnemyDefeat()
    {
        if(health == 0)
        {
            Destroy(gameObject);
        }
    }
}
