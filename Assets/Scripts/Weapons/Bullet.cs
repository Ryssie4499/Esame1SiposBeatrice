using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject EnemyWeapon;
    public GameObject PlayerWeapon;
    public int speed;
    public int bulletSpeed;
    void Update()
    {
        PlayerWeapon.transform.Translate(Vector3.right * speed * Time.deltaTime);
        EnemyWeapon.transform.Translate(Vector3.left * bulletSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
