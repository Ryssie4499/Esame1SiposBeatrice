using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject player;
    [SerializeField] float playerSpeed = 15f;                   //velocit� del player editabile

    [Header("Camera")]
    [SerializeField] float boundaryHeight = 6f;                 //altezza contorni editabile

    [Header("Stats")]
    public int maxHP = 100;                                     //punti vita massimi
    public int health;                                          //punti vita attuali
    public int trapDamage = 10;                                 //danno inflitto al player dalle trappole sganciate dall'enemy


    [Header("Collectible")]
    public int gemCount;                                        //quantit� di collectible raccolti
    public int XPCount;
    

    [SerializeField] GameObject BossHealthBar;

    Vector2 pOldPos;                                            //posizioni vecchie per i boundaries
    Vector2 pStartPos;

    [HideInInspector] public int score;
    public bool l2, l3;

    CameraMove cM;
    GameManager GM;
    UIManager UM;
    private void Start()
    {
        //setto la vita del player iniziale uguale al massimo e la posizione iniziale alla posizione attuale del player
        l2 = false;
        l3 = false;

        health = maxHP;
        pStartPos = player.transform.position;

        cM = FindObjectOfType<CameraMove>();
        GM = FindObjectOfType<GameManager>();
        UM = FindObjectOfType<UIManager>();
        GameManager.Record += ScoreRecord;
    }
    private void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            MovePlayer();
        }
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
            score = GM._score + 20;
            ScoreRecord();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("HP"))
        {
            if (health <= (maxHP - 20))
            {
                health += 20;
                Destroy(other.gameObject);
            }
        }
        if (other.CompareTag("StartLevel_2"))
        {
            GM.gameStatus = GameManager.GameStatus.gameRunning;
            l2 = true;
            Destroy(other);
        }
        if (other.CompareTag("StartLevel_3"))
        {
            GM.gameStatus = GameManager.GameStatus.gameRunning;
            l3 = true;
            Destroy(other);
        }
        if (other.CompareTag("StopCamera"))
        {
            BossHealthBar.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EndLevel"))
        {
            GM.gameStatus = GameManager.GameStatus.gameLevelEnd;
        }
        if (other.CompareTag("StopCamera"))
        {
            cM.cameraStop = true;
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
        GM.gameStatus = GameManager.GameStatus.gameOver;
        if (l2 == false && l3 == false)
        {
            SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
            gemCount = 0;
            health = maxHP;
        }
        else if (l2 == true && l3 == false)
        {
            SceneManager.LoadScene("Level_2", LoadSceneMode.Single);
            gemCount = 0;
            health = maxHP;
        }
        else if (l2= true && l3==true)
        {
            SceneManager.LoadScene("Level_3", LoadSceneMode.Single);
            gemCount = 0;
            health = maxHP;
        }
    }

    
    public void ScoreRecord()
    {
        GM._score = score;
    }
}

