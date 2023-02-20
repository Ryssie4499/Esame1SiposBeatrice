using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject tutorialCanvas;
    [SerializeField] public GameObject tutorialMove;
    [SerializeField] public GameObject tutorialShoot;
    [SerializeField] public GameObject tutorialBomb;
    [SerializeField] public GameObject startCanvas;
    [SerializeField] public GameObject levelEndCanvas;
    [SerializeField] public GameObject endCanvas;
    [SerializeField] public GameObject pauseCanvas;

    [SerializeField] public TextMeshProUGUI gemsText;
    [SerializeField] public TextMeshProUGUI XPText;
    [SerializeField] public TextMeshProUGUI totText;
    [SerializeField] public TextMeshProUGUI pointsText;
    [SerializeField] public TextMeshProUGUI TotPointsText;

    [SerializeField] Image ShootingBar;
    [SerializeField] Image HPBar, BossHPBar;

    [HideInInspector] public int pressNum, numGems;
    [HideInInspector] public int score;

    Score sc;
    BossManager bM;
    PlayerMovement pM;
    Muzzle m;
    GameManager GM;

    void Start()
    {
        sc = FindObjectOfType<Score>();
        bM = FindObjectOfType<BossManager>();
        pM = FindObjectOfType<PlayerMovement>();
        m = FindObjectOfType<Muzzle>();
        GM = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            MoveTutorial();
            ShootingTutorial();
            BombTutorial();
            XPText.text = pM.XPCount.ToString("000");
            //score = sc.CurrentScore;
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            totText.text = pM.score.ToString("000");
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            gemsText.text = pM.gemCount.ToString();
            ShootingBar.fillAmount = m.numColpi / m.numMaxColpi;
            HPBar.fillAmount = (float)pM.health / pM.maxHP;
            BossHPBar.fillAmount = (float)bM.health / bM.maxHP;
        }

        if (GM.gameStatus == GameManager.GameStatus.gameLevelEnd)
        {
            levelEndCanvas.SetActive(true);
            pointsText.text = pM.score.ToString("000");
        }
        if(GM.gameStatus == GameManager.GameStatus.gameEnd)
        {
            TotPointsText.text = pM.score.ToString("0000");
        }
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
    public void Beginning()
    {
        startCanvas.SetActive(false);
        GM.gameStatus = GameManager.GameStatus.gameRunning;
    }

    public void NextLevel()
    {
        levelEndCanvas.SetActive(false);
        GM.gameStatus = GameManager.GameStatus.gameRunning;
        Debug.Log("Funziono!");
        if (pM.levelCount == 0)
        {
            levelEndCanvas.SetActive(false);
            Debug.Log("Funziono X2!");
            SceneManager.LoadScene("Level_2", LoadSceneMode.Single);
            //GM.EndLevel();
            pM.ScoreRecord();
        }
        if (pM.levelCount == 1)
        {
            levelEndCanvas.SetActive(false);
            Debug.Log("Funziono X3!");
            SceneManager.LoadScene("Level_3", LoadSceneMode.Single);
            //GM.EndLevel();
            pM.ScoreRecord();
        }
    }

    public void Continue()
    {
        GM.gameStatus = GameManager.GameStatus.gameRunning;
        pauseCanvas.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
        pM.gemCount = 0;
        pM.health = pM.maxHP;
        GM.gameStatus = GameManager.GameStatus.gameRunning;
        pauseCanvas.SetActive(false);
    }
}
