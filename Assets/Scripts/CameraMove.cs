using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [HideInInspector] public bool cameraStop;
    [SerializeField] float cameraSpeed;                         //velocità della camera editabile
    
    void Update()
    {
        //la camera si muove a velocità costante verso destra
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
