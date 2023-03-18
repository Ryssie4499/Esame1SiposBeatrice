using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
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
            Shoot();
        }
    }

    public override void Shoot()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            Instantiate(EnemyWeapon, new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z), Quaternion.identity);
            spawnTimer = 0;
        }
    }
    protected override void OnCollisionEnter(Collision coll)
    {
        base.OnCollisionEnter(coll);
    }

}
