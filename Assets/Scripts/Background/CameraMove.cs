using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [HideInInspector] public bool cameraStop;                   //a contatto col trigger della fine del livello, la booleana diventa true
    [SerializeField] float cameraSpeed;                         //velocità della camera editabile

    GameManager GM;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            //la camera si muove a velocità costante verso destra
            if (cameraStop == false)
            {
                Camera.main.transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
            }
            //la velocità della camera si azzera e la visuale si ferma
            else
            {
                Camera.main.transform.Translate(Vector3.right * 0 * Time.deltaTime);
            }
        }
    }

}
