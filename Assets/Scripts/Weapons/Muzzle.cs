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
    float numColpi;
    float spawnTimer;
    PlayerMovement pM;

    private void Start()
    {
        numColpi = 10;
        pM = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        spawnPosition = new Vector3(muzzle.transform.position.x, muzzle.transform.position.y, muzzle.transform.position.z);
        spawnTimer += Time.deltaTime;
        if (Input.GetButton("Jump"))
        {
            if (spawnTimer >= spawnRate && numColpi>0)
            {
                Instantiate(bulletToSpawn, spawnPosition, Quaternion.identity);
                numColpi--;
                spawnTimer = 0;
            }
            else if (numColpi <=0)
            {
                StartCoroutine(maxColpi());
            }
        }
        else if (Input.GetButtonDown("Jump"))
        {
            if(spawnTimer >= spawnRate && numColpi > 0)
            {
                Instantiate(bulletToSpawn, spawnPosition, Quaternion.identity);
                numColpi--;
                spawnTimer = 0;
            }
            else
            {
                StartCoroutine(maxColpi());
            }
        }
        else if(spawnTimer >= spawnRate && pM.gemCount > 0 && Input.GetKeyDown(KeyCode.E) )
        {
            Instantiate(bombToSpawn, spawnPosition, Quaternion.identity);
            spawnTimer = 0;
        }

    }

    IEnumerator maxColpi()
    {
        yield return new WaitForSeconds(3);
        numColpi = 10;
    }
}
