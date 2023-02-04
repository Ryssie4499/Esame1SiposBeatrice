using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject player;

    //[SerializeField] float playerSpeed;
    [SerializeField] float cameraSpeed;

    [SerializeField] float speed = 15f;             //velocità editabile
    [SerializeField] float boundaryHeight = 5f;     //altezza contorni editabile

    Vector2 pOldPos;                     //posizioni vecchie per i boundaries
    Vector2 pStartPos;

    private void Start()
    {
        pStartPos = player.transform.position;
    }
    private void Update()
    {
        //transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
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
    
}
