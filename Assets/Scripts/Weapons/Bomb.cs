using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int speed;
    PlayerMovement pM;
    GameManager GM;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        pM = FindObjectOfType<PlayerMovement>();
    }
    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
