using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject player;
    [SerializeField] float playerSpeed = 15f;

    [Header("Camera")]
    [SerializeField] float boundaryHeight = 6f;                 //altezza contorni editabile

    [Header("Stats")]
    public int maxHP = 100;
    public int health;
    public int trapDamage = 10;                                 //danno che le trappole/proiettili sganciati dagli enemy infliggono al player


    [Header("Collectible")]
    public int gemCount;
    public int XPCount;

    [Header("Boss")]
    [SerializeField] GameObject BossHealthBar;

    [Header("SFX")]
    public AudioSource HealSound;
    public AudioSource HitSound;
    public AudioSource Soundtrack;

    Vector2 pOldPos;                                            //posizioni vecchie per i boundaries
    Vector2 pStartPos;

    [HideInInspector] public int score;
    [HideInInspector] public bool l2, l3;                       //livello 2 e livello 3 disattivati di default

    //references
    GameManager GM;

    private void Start()
    {
        //setto la vita del player iniziale uguale al massimo e la posizione iniziale alla posizione attuale del player
        health = maxHP;
        pStartPos = player.transform.position;

        //salvo il punteggio totale per i livelli successivi
        GM = FindObjectOfType<GameManager>();
        GameManager.Record += ScoreRecord;
    }
    private void Update()
    {
        //solo se il gioco è in play, il player può muoversi e il volume iniziale del soundtrack è più basso
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            MovePlayer();
            Soundtrack.volume = 0.4f;
            StartCoroutine(SoundVolume());
        }

        //quando il gioco non è in play (pausa, fine livello, fine gioco o sconfitta) il volume del soundtrack è quasi impercettibile
        if (GM.gameStatus != GameManager.GameStatus.gameRunning)
        {
            Soundtrack.volume = 0.3f;
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

    private void OnTriggerEnter(Collider other)
    {
        //se il player entra nel trigger della gemma, il suo contatore aumenta e la gemma si disattiva
        if (other.CompareTag("BombGem"))
        {
            gemCount++;
            other.gameObject.SetActive(false);
        }

        //se il player entra nel trigger dei punti esperienza (sfere azzurre), il punteggio aumenta e la sfera azzurra si disattiva
        if (other.CompareTag("XP"))
        {
            XPCount += 20;                          //questo punteggio si resetta in ogni livello
            score = GM._score + 20;                 //questo si mantiene da un livello all'altro
            ScoreRecord();
            other.gameObject.SetActive(false);
        }

        //se il player entra nel trigger dei cuori dorati, la vita aumenta e il cuore si disattiva
        if (other.CompareTag("HP"))
        {
            if (health <= (maxHP - 20))             //solo se la vita del player è minore del totale meno la quantità di vita recuperabile, il cuore viene raccolto
            {
                health += 20;
                HealSound.Play();                   //ogni volta che la vita aumenta grazie ai cuori raccolti, c'è un effetto sonoro
                Destroy(other.gameObject);
            }
        }

        //se il player entra nel trigger dell'inizio livello 2, il gioco va in play e il trigger si distrugge
        if (other.CompareTag("StartLevel_2"))
        {
            //GM.gameStatus = GameManager.GameStatus.gameRunning;
            l2 = true;
            Destroy(other);
        }

        //se il player entra nel trigger dell'inizio livello 3, il gioco va in play e il trigger si distrugge
        if (other.CompareTag("StartLevel_3"))
        {
            //GM.gameStatus = GameManager.GameStatus.gameRunning;
            l3 = true;
            Destroy(other);
        }

        //se il player entra nel trigger dell'EndLevel alla fine del terzo livello, compare la UI della vita del Boss
        if (other.CompareTag("StopCamera"))
        {
            BossHealthBar.SetActive(true);
        }

        //se il player entra nel trigger dell'EndLevel del primo e del secondo livello, lo status di gioco diventa gameLevelEnd (quindi si attiverà il menù di fine livello)
        if (other.CompareTag("EndLevel"))
        {
            GM.gameStatus = GameManager.GameStatus.gameLevelEnd;
        }
    }

    //se il player collide con la trappola droppata dall'enemy, si aggiorna la vita del player e la trappola si disattiva
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("EnemyWeapon"))
        {
            //ad ogni colpo subito dal player corrisponde un suono 
            HitSound.Play();

            //se gli HP del player sono > 0 i punti vita diminuiscono del valore dato dal danno della trappola
            if (health > 0)
            {
                health -= trapDamage;
            }
            //se gli HP sono <= 0 il player muore
            if (health <= 0)
            {
                Death();
            }

            //il proiettile si distrugge
            Destroy(coll.gameObject);
        }

        //quando il player collide con l'Enemy, perde vita e produce un effetto sonoro
        if (coll.collider.CompareTag("Enemy"))
        {
            HitSound.Play();
            if (health > 0)
            {
                health -= trapDamage;
            }
            if (health <= 0)
            {
                Death();
            }
        }
    }

    //alla morte del player, il gioco va in GameOver
    public void Death()
    {
        GM.gameStatus = GameManager.GameStatus.gameOver;
    }

    //lo score viene aggiornato e mantenuto grazie al GameManager sempre presente in scena (e mai distruttibile nel passaggio tra una scena e l'altra)
    public void ScoreRecord()
    {
        GM._score = score;
    }

    //per i primi due secondi il soundtrack avrà un volume basso, dopo quei secondi, aumenta
    public IEnumerator SoundVolume()
    {
        yield return new WaitForSeconds(2);
        Soundtrack.volume = 0.8f;
    }
}

