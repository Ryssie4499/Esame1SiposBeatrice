using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject tutorialCanvas;
    [SerializeField] public GameObject tutorialMove;
    [SerializeField] public GameObject tutorialShoot;
    [SerializeField] public GameObject tutorialBomb;
    [SerializeField] Image ShootingBar;
    public int pressNum, numGems;
    PlayerMovement pM;
    Muzzle m;
    void Start()
    {
        pM = FindObjectOfType<PlayerMovement>();
        m = FindObjectOfType<Muzzle>();
    }
    void Update()
    {
        MoveTutorial();
        ShootingTutorial();
        BombTutorial();
        ShootingBar.fillAmount = m.numColpi / m.numMaxColpi;
    }
    public void MoveTutorial()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            pressNum++;
            tutorialMove.SetActive(false);
            if (pressNum == 1)
            {
                tutorialShoot.SetActive(true);
            }
        }
    }
    public void ShootingTutorial()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutorialShoot.SetActive(false);
        }
    }
    public void BombTutorial()
    {
        if (numGems == 0)
        {
            if (pM.gemCount == 1)
            {
                tutorialBomb.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    tutorialBomb.SetActive(false);
                    numGems++;
                }
            }
        }
    }
    
}
