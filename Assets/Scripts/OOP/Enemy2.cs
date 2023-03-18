using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }
    public override void Movement()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }
    public override void Shoot()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            Instantiate(EnemyWeapon, new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z), Quaternion.identity);
            spawnTimer = 0;
        }
    }
    protected override void OnCollisionEnter(Collision coll)
    {
        base.OnCollisionEnter(coll);
    }

}
