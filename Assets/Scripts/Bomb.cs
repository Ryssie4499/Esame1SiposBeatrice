using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int speed;
    PlayerMovement pM;
    private void Start()
    {
        pM = FindObjectOfType<PlayerMovement>();
    }
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            pM.gemCount--;
        }
    }
}
