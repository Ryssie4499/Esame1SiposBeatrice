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
    public int bulletDamage = 10;
    public int bombDamage = 30;
    public int bulletPoints, bombPoints; //, totBullet, totBomb, totXP
    int chance;
    private void Start()
    {
        health = maxHP;
    }

    void Update()
    {
        //XPPoints();
        //totBullet += bulletPoints;
        //totBomb += bombPoints;
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {
            bulletPoints += 20;
            if (health > 0)
                health -= bulletDamage;
            if (health <= 0)
            {
                chance = Random.Range(0, 4);
                if (chance == 3)
                {
                    Instantiate(BombGem, transform.position, Quaternion.identity);
                }

                else if (chance == 0)
                {
                    Instantiate(EnemyWeapon, transform.position, Quaternion.identity);
                }
                EnemyDefeat();
            }
        }
        if (coll.collider.CompareTag("Bomb"))
        {
            bombPoints += 40;
            if (health > 0)
                health -= bombDamage;
            if (health <= 0)
            {
                EnemyDefeat();
            }
        }
    }
    public void EnemyDefeat()
    {
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
    //public void XPPoints()
    //{
    //    totXP = totBullet + totBomb;
    //}
}
