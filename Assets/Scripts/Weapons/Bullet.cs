using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Type of Bullets")]
    public GameObject EnemyWeapon;
    public GameObject PlayerWeapon;

    [Header("Bullets' Speed")]
    public int speed;
    public int bulletSpeed;

    //references
    GameManager GM;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }

    //solo se il gioco è in play, i proiettili si muovono (quelli del player verso destra e quelli degli enemy verso sinistra)
    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            PlayerWeapon.transform.Translate(Vector3.right * speed * Time.deltaTime);
            EnemyWeapon.transform.Translate(Vector3.left * bulletSpeed * Time.deltaTime);
        }
    }

    //quando il proiettile colpisce un qualsiasi oggetto in scena, si autodistrugge
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
