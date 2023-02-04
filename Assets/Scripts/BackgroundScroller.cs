using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float tileSize;
    [SerializeField] [Range(0, 1)] float scrollSpeed;                                                  //a prova di Designer
    [SerializeField] float viewZone = 1;                                                            //metro
    //references
    private Transform cameraTransform;
    private Transform[] tiles;
    private int leftIndex, rightIndex;
    private float lastCameraX;
    
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        tiles = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            tiles[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = tiles.Length - 1;                                                              //perchè primo 0 non 1
    }

    void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * scrollSpeed);                               //1,0,0 right
        lastCameraX = cameraTransform.position.x;

        if (cameraTransform.position.x > (tiles[rightIndex].transform.position.x - viewZone))
        {
            SwitchRight();
        }


    }
    private void SwitchRight()
    {
        if (tiles.Length < 2) return;
        //sposto tile a sinistra
        tiles[leftIndex].position = Vector3.right * (tiles[rightIndex].position.x + tileSize);

        //aggiorno indici
        rightIndex = leftIndex;
        leftIndex++;

        //controllo che l'indice non sfori la size dell'array
        if (leftIndex == tiles.Length)
        {
            leftIndex = 0;
        }
    }
}
