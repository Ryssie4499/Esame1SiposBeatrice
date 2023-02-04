using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public int maxHP = 30;
    public int health;
    public int movementSpeed;
    public int bulletDamage = 30;
    //Bullet bul;
    private void Start()
    {
        health = maxHP;
        //bul = FindObjectOfType<Bullet>();
        gameObject.SetActive(true);
    }

    void Update()
    {
        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Bullet"))
        {
            if (health > 0)
                health -= bulletDamage;
            if (health == 0)
            {
                EnemyDefeat();
            }

        }
    }
    public void EnemyDefeat()
    {
        if(health == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
