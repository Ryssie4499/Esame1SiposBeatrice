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
    public int movementSpeed, bulletSpeed;
    public int bulletDamage = 10;
    public int bombDamage = 30;
    int chance;
    //int numOfBullets;
    //float spawnTimer;
    PlayerMovement pM;
    private void Start()
    {
        pM = FindObjectOfType<PlayerMovement>();
        health = maxHP;
    }

    void Update()
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        //EnemyBullet();
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

                else if (chance == 0 && pM.levelCount == 0)
                {
                    Instantiate(EnemyWeapon, transform.position, Quaternion.identity);
                }
                EnemyDefeat();
            }
        }
        if (coll.collider.CompareTag("Bomb"))
        {
            Instantiate(XP, transform.position, Quaternion.identity);
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
    //public void EnemyBullet()
    //{
    //    if (pM.levelCount == 1)
    //    {
    //        StartCoroutine(timeToShoot());
    //        spawnTimer += Time.deltaTime;
    //        if (numOfBullets < 1)
    //        {
    //            Instantiate(EnemyWeapon, new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z), Quaternion.identity);
    //            spawnTimer = 0;
    //            numOfBullets++;
    //        }
    //        EnemyWeapon.transform.Translate(Vector3.left * bulletSpeed * Time.deltaTime);
    //    }
    //}
    //IEnumerator timeToShoot()
    //{
    //    yield return new WaitForSeconds(20);
    //}
}
