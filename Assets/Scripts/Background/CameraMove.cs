using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [HideInInspector] public bool cameraStop;                   //a contatto col trigger della fine del livello, la booleana diventa true
    [SerializeField] float cameraSpeed;                         //velocità della camera editabile

    //references
    GameManager GM;
    PlayerMovement pM;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        pM = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            //se la camera raggiunge la posizione ottimale per avere nella visuale il boss, si ferma
            if (Camera.main.transform.position.x >= 90.5f && pM.l3 == true)
            {
                cameraStop = true;
            }
            //la camera si muove a velocità costante verso destra
            if (cameraStop == false)
            {
                Camera.main.transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
            }
            //la velocità della camera si azzera e la visuale si ferma
            else if (cameraStop == true && pM.l3 == true)
            {
                Camera.main.transform.Translate(Vector3.right * 0 * Time.deltaTime);
            }
        }
    }
}
