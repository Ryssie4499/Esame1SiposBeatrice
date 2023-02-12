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
    [SerializeField] public TextMeshProUGUI gemsText;
    [SerializeField] public TextMeshProUGUI XPText;
    [SerializeField] Image ShootingBar;
    [SerializeField] Image HPBar, BossHPBar;
    [HideInInspector] public int pressNum, numGems;

    BossManager bM;
    PlayerMovement pM;
    Muzzle m;
    void Start()
    {
        bM = FindObjectOfType<BossManager>();
        pM = FindObjectOfType<PlayerMovement>();
        m = FindObjectOfType<Muzzle>();
    }
    void Update()
    {
        MoveTutorial();
        ShootingTutorial();
        BombTutorial();
        XPText.text = pM.XPCount.ToString("000");
        gemsText.text = pM.gemCount.ToString();
        ShootingBar.fillAmount = m.numColpi / m.numMaxColpi;
        HPBar.fillAmount = (float)pM.health / pM.maxHP;
        BossHPBar.fillAmount = (float)bM.health / bM.maxHP;
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
                numGems++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && numGems == 1)
        {
            tutorialBomb.SetActive(false);
        }
    }
    
    
}
