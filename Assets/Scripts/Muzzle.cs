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
    float spawnTimer;
    

    private void Start()
    {

    }
    private void Update()
    {
        spawnPosition = new Vector3(muzzle.transform.position.x, muzzle.transform.position.y, muzzle.transform.position.z);
        spawnTimer += Time.deltaTime;
        if (Input.GetButton("Jump"))
        {
            if (spawnTimer >= spawnRate)
            {
                Instantiate(bulletToSpawn, spawnPosition, Quaternion.identity);
                spawnTimer = 0;
            }
        }
        else if (Input.GetButtonDown("Jump"))
        {
            Instantiate(bulletToSpawn, spawnPosition, Quaternion.identity);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            Instantiate(bombToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}

