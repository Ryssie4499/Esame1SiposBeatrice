using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    //[SerializeField] float playerSpeed;
    [SerializeField] float cameraSpeed;

    [SerializeField] float speed = 15f;             //velocità editabile
    [SerializeField] float boundaryHeight = 5f;     //altezza contorni editabile

    Vector2 pOldPos;                     //posizioni vecchie per i boundaries
    Vector2 pStartPos;
    public int maxHP = 100;
    public int health;
    public int trapDamage = 10;
    public int gemCount;
    
    private void Start()
    {
        health = maxHP;
        pStartPos = player.transform.position;
    }
    private void Update()
    {
        Camera.main.transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        MovePlayer();
    }

    private void MovePlayer()
    {
        float p1VMove = Input.GetAxis("Vertical");                          //si muove con i tasti ws
        float p1HMove = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(p1HMove, p1VMove);
        player.transform.Translate(move * Time.deltaTime * speed);   //spostamento player1
        

        //controllo posizione
        if (Mathf.Abs(player.transform.position.y) > boundaryHeight)
        {
            player.transform.position = new Vector2(pOldPos.x, Mathf.RoundToInt(pOldPos.y));
        }

        //aggiornamento posizione al frame precedente
        pOldPos = player.transform.position;
        
    }
    public void ResetPlayerPositions()
    {
        player.transform.position = pStartPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BombGem"))
        {
            gemCount++;
            other.gameObject.SetActive(false);
        }

    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("EnemyWeapon"))
        {
            if (health > 0)
            {
                health -= trapDamage;
                Debug.Log(health);
            }
            if (health <= 0)
            {
                Death();
            }
            coll.gameObject.SetActive(false);
        }
    }
    public void Death()
    {
        if (health == 0)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
            gemCount = 0;
            health = maxHP;
            gameObject.SetActive(true);
        }
    }

}
