using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float tileSize;                                            //misura della tile
    [SerializeField] [Range(0, 1)] float scrollSpeed;                           //velocità di movimento delle tiles da 0 a 1
    [SerializeField] float viewZone = 1;                                        //zona di visuale entro la quale avviene lo scambio

    //references
    private Transform cameraTransform;                                          //posizione della camera
    private Transform[] tiles;                                                  //array formato dalle posizioni delle tiles
    private int leftIndex, rightIndex;                                          //indici che fanno riferimento alle tiles
    private float lastCameraX;                                                  //posizione precedente della camera sull'asse x

    GameManager GM;
    void Start()
    {
        //assegno la posizione della nostra main camera al cameraTransform e al lastCameraX
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;

        //faccio in modo che le tiles a cui fa riferimento lo script, siano gli elementi figli del parent che contiene lo script, indipendentemente da quanti sono
        tiles = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            tiles[i] = transform.GetChild(i);
        }

        //assegno i valori iniziali degli indici
        leftIndex = 0;
        rightIndex = tiles.Length - 1;

        GM = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {

            //il movimento della camera è dato dalla velocità data da inspector, la distanza tra la precedente coordinata x e quella successiva e dal vettore (1,0,0)
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * scrollSpeed);
            lastCameraX = cameraTransform.position.x;

            //se la posizione della camera è all'interno della viewzone, la tile a sinistra si sposta a destra
            if (cameraTransform.position.x > (tiles[rightIndex].transform.position.x - viewZone))
            {
                SwitchRight();
            }
        }
    }
    private void SwitchRight()
    {
        if (tiles.Length < 2) return;

        //sposto tile da sinistra a destra della loro stessa misura sommata a quella della tile che al momento sta a destra
        tiles[leftIndex].position = Vector3.right * (tiles[rightIndex].position.x + tileSize);

        //aggiorno gli indici
        rightIndex = leftIndex;
        leftIndex++;

        //controllo che l'indice non sfori la size dell'array
        if (leftIndex == tiles.Length)
        {
            leftIndex = 0;
        }
    }
}
