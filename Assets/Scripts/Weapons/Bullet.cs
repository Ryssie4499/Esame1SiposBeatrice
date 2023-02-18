using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject EnemyWeapon;
    public GameObject PlayerWeapon;
    public int speed;
    public int bulletSpeed;
    GameManager GM;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            PlayerWeapon.transform.Translate(Vector3.right * speed * Time.deltaTime);
            EnemyWeapon.transform.Translate(Vector3.left * bulletSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
