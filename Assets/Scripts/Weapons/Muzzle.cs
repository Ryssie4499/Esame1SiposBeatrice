using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    [Header("Bullets & Bombs")]
    [SerializeField] public GameObject bulletToSpawn;
    [SerializeField] public GameObject bombToSpawn;
    [SerializeField] public GameObject muzzle;
    [SerializeField] public float spawnRate;

    [Header("SFX")]
    public AudioSource ShootingSound;
    public AudioSource BombSound;

    //posizione di spawn proiettili/bombe e numero di colpi disponibili/massimi
    [HideInInspector] public Vector3 spawnPosition;
    [HideInInspector] public float numMaxColpi = 20;
    [HideInInspector] public float numColpi;

    float spawnTimer;

    //references
    PlayerMovement pM;
    GameManager GM;

    private void Start()
    {
        //il numero di colpi iniziale è uguale a quello massimo
        numColpi = numMaxColpi;
        pM = FindObjectOfType<PlayerMovement>();
        GM = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        //solo se il gioco è in play si può sparare
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            //posizione e tempo di spawn sia dei proiettili che delle bombe
            spawnPosition = new Vector3(muzzle.transform.position.x, muzzle.transform.position.y, muzzle.transform.position.z);
            spawnTimer += Time.deltaTime;

            //se tengo premuto il tasto SPACE...
            if (Input.GetButton("Jump"))
            {
                if (spawnTimer >= spawnRate && numColpi > 0)
                {
                    //spawnano i proiettili e il numero di colpi rimanenti diminuisce
                    Instantiate(bulletToSpawn, spawnPosition, Quaternion.identity);
                    numColpi--;
                    spawnTimer = 0;

                    //ad ogni proiettile spawnato, corrisponde un suono
                    ShootingSound.Play();
                }

                //una volta finiti i colpi, bisogna aspettare qualche secondo prima che si ricarichino
                else if (numColpi <= 0)
                {
                    StartCoroutine(maxColpi());
                }
            }

            //ogni volta che premo il tasto SPACE...
            else if (Input.GetButtonDown("Jump"))
            {
                if (spawnTimer >= spawnRate && numColpi > 0)
                {
                    //spawnano i proiettili e il tempo restante prima che i proiettili finiscano diminuisce
                    Instantiate(bulletToSpawn, spawnPosition, Quaternion.identity);
                    StartCoroutine(timerColpi());
                    spawnTimer = 0;

                    //ad ogni proiettile spawnato, corrisponde un suono
                    ShootingSound.Play();
                }

                //una volta finiti i colpi, bisogna aspettare qualche secondo prima che si ricarichino
                else
                {
                    StartCoroutine(maxColpi());
                }
            }

            //se il numero di gemme è maggiore di zero, basta cliccare il tasto E per far spawnare le bombe e farne diminuire la quantità nell'inventario
            else if (spawnTimer >= spawnRate && pM.gemCount > 0 && Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(bombToSpawn, spawnPosition, Quaternion.identity);
                pM.gemCount--;
                spawnTimer = 0;

                //ad ogni bomba spawnata, corrisponde un suono
                BombSound.Play();
            }
        }
    }

    //dopo due secondi dall'esaurimento dei proiettili, il numero di colpi disponibili torna al massimo
    IEnumerator maxColpi()
    {
        yield return new WaitForSeconds(2);
        numColpi = numMaxColpi;
    }

    //dopo quattro secondi dal primo colpo, il numero di proiettili rimanenti si esaurisce (il timer funziona solo quando viene premuto il tasto SPACE, perciò non si esaurisce dopo un solo colpo)
    IEnumerator timerColpi()
    {
        yield return new WaitForSeconds(4);
        numColpi = 0;
    }

}

