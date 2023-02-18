using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    [SerializeField] public GameObject bulletToSpawn;
    [SerializeField] public GameObject bombToSpawn;
    [SerializeField] public GameObject muzzle;
    [SerializeField] float spawnRate;
    [HideInInspector] public Vector3 spawnPosition;
    public float numMaxColpi = 20;
    public float numColpi;
    float spawnTimer;
    PlayerMovement pM;
    GameManager GM;
    private void Start()
    {
        numColpi = numMaxColpi;
        pM = FindObjectOfType<PlayerMovement>();
        GM = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            spawnPosition = new Vector3(muzzle.transform.position.x, muzzle.transform.position.y, muzzle.transform.position.z);
            spawnTimer += Time.deltaTime;
            if (Input.GetButton("Jump"))
            {
                if (spawnTimer >= spawnRate && numColpi > 0)
                {
                    Instantiate(bulletToSpawn, spawnPosition, Quaternion.identity);
                    numColpi--;
                    spawnTimer = 0;
                }
                else if (numColpi <= 0)
                {
                    StartCoroutine(maxColpi());
                }
            }
            else if (Input.GetButtonDown("Jump"))
            {
                if (spawnTimer >= spawnRate && numColpi > 0)
                {
                    StartCoroutine(timerColpi());
                    Instantiate(bulletToSpawn, spawnPosition, Quaternion.identity);
                    spawnTimer = 0;
                }
                else
                {
                    StartCoroutine(maxColpi());
                }
            }
            else if (spawnTimer >= spawnRate && pM.gemCount > 0 && Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(bombToSpawn, spawnPosition, Quaternion.identity);
                spawnTimer = 0;
            }
        }
    }

    IEnumerator maxColpi()
    {
        yield return new WaitForSeconds(2);
        numColpi = numMaxColpi;
    }
    IEnumerator timerColpi()
    {
        yield return new WaitForSeconds(4);
        numColpi = 0;
    }

}

