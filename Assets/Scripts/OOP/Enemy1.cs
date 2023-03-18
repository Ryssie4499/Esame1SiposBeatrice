using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    public override void Movement()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
    }
    
    protected override void OnCollisionEnter(Collision coll)
    {
        base.OnCollisionEnter(coll);
    }

}
