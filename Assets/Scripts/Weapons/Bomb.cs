using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //velocità delle bombe
    public int speed;

    //references
    PlayerMovement pM;
    GameManager GM;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        pM = FindObjectOfType<PlayerMovement>();
    }

    //solo se il gioco è in play, la bomba si potrà muovere verso destra
    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    //se la bomba colpisce un qualsiasi oggetto in scena, si autodistrugge
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
