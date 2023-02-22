using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [HideInInspector] public bool cameraStop;
    [SerializeField] float cameraSpeed;                         //velocit� della camera editabile
    GameManager GM;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {

            //la camera si muove a velocit� costante verso destra
            if (cameraStop == false)
            {
                Camera.main.transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
            }
            else
            {
                Camera.main.transform.Translate(Vector3.right * 0 * Time.deltaTime);
            }
        }
    }

}