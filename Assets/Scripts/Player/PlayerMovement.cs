using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject player;
    [SerializeField] float playerSpeed = 15f;                   //velocità del player editabile

    [Header("Camera")]
    [SerializeField] float cameraSpeed;                         //velocità della camera editabile
    [SerializeField] float boundaryHeight = 5f;                 //altezza contorni editabile

    [Header("Stats")]
    public int maxHP = 100;                                     //punti vita massimi
    public int health;                                          //punti vita attuali
    public int trapDamage = 10;                                 //danno inflitto al player dalle trappole sganciate dall'enemy

    [Header("Collectible")]
    public int gemCount;                                        //quantità di collectible raccolti
    public int XPCount;
    public int levelCount;

    Vector2 pOldPos;                                            //posizioni vecchie per i boundaries
    Vector2 pStartPos;

    private void Start()
    {
        //setto la vita del player iniziale uguale al massimo e la posizione iniziale alla posizione attuale del player

        health = maxHP;
        pStartPos = player.transform.position;
    }
    private void Update()
    {
        //la camera si muove a velocità costante verso destra

        Camera.main.transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        MovePlayer();
    }

    private void MovePlayer()
    {
        //il player si muove con W ed S sull'asse verticale e con A e D sull'asse orizzontale

        float p1VMove = Input.GetAxis("Vertical");
        float p1HMove = Input.GetAxis("Horizontal");

        //creo un vettore 2D in cui inserire il movimento orizzontale e verticale e lo utilizzo per muovere il player

        Vector2 move = new Vector2(p1HMove, p1VMove);
        player.transform.Translate(move * Time.deltaTime * playerSpeed);

        //controllo la posizione rispetto ai limiti della mappa (upper e lower insieme)

        if (Mathf.Abs(player.transform.position.y) > boundaryHeight)
        {
            player.transform.position = new Vector2(pOldPos.x, Mathf.RoundToInt(pOldPos.y));
        }

        //aggiornamento della posizione al frame precedente quando cerca di sforare dai limiti di mappa

        pOldPos = player.transform.position;
    }

    //resetto la posizione del player a quella iniziale

    public void ResetPlayerPositions()
    {
        player.transform.position = pStartPos;
    }

    //se il player entra nel trigger della gemma droppata dall'enemy, il suo contatore aumenta e la gemma si disattiva

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BombGem"))
        {
            gemCount++;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("XP"))
        {
            XPCount += 20;
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("EndLevel"))
        {
            SceneManager.LoadScene("Level_2", LoadSceneMode.Single);
        }
        if (other.CompareTag("StartLevel"))
        {
            levelCount=1;
        }
    }

    //se il player collide con la trappola droppata dall'enemy, si aggiorna la vita del player e la trappola si disattiva

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("EnemyWeapon"))
        {
            if (health > 0)
            {
                health -= trapDamage;                           //se gli HP del player sono > 0 i punti vita diminuiscono del valore dato dal danno della trappola
            }
            if (health <= 0)
            {
                Death();                                        //se gli HP sono <= 0 il player muore
            }
            coll.gameObject.SetActive(false);
        }
        if (coll.collider.CompareTag("Enemy"))
        {
            if (health > 0)
            {
                health -= trapDamage;                           //se gli HP del player sono > 0 i punti vita diminuiscono del valore dato dal danno dell'enemy
            }
            if (health <= 0)
            {
                Death();                                        //se gli HP sono <= 0 il player muore
            }
        }
    }

    //alla morte del player, la scena si resetta e il gioco ricomincia da capo, senza collectible e con i massimi HP

    public void Death()
    {
        if (health == 0)
        {
            if (levelCount == 0)
            {
                SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
                gemCount = 0;
                health = maxHP;
            }
            else if (levelCount == 1)
            {
                SceneManager.LoadScene("Level_2", LoadSceneMode.Single);
                gemCount = 0;
                health = maxHP;
            }
        }
    }

}
