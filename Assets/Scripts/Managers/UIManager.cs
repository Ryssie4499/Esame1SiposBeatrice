using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] public GameObject tutorialCanvas;
    [SerializeField] public GameObject tutorialMove;
    [SerializeField] public GameObject tutorialShoot;
    [SerializeField] public GameObject tutorialBomb;
    [SerializeField] public GameObject startCanvas;
    [SerializeField] public GameObject levelEndCanvas;
    [SerializeField] public GameObject endCanvas;
    [SerializeField] public GameObject pauseCanvas;
    [SerializeField] public GameObject defeatCanvas;

    [Header("Text")]
    [SerializeField] public TextMeshProUGUI gemsText;
    [SerializeField] public TextMeshProUGUI XPText;
    [SerializeField] public TextMeshProUGUI totText;
    [SerializeField] public TextMeshProUGUI pointsText;
    [SerializeField] public TextMeshProUGUI TotPointsText;

    [Header("Images")]
    [SerializeField] Image ShootingBar;
    [SerializeField] Image HPBar, BossHPBar;

    [HideInInspector] public int pressNum, numGems;             //numero di WASD premuti per la prima volta, numero di gemme ottenute per la prima volta

    //references
    BossManager bM;
    PlayerMovement pM;
    Muzzle m;
    GameManager GM;

    void Start()
    {
        bM = FindObjectOfType<BossManager>();
        pM = FindObjectOfType<PlayerMovement>();
        m = FindObjectOfType<Muzzle>();
        GM = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        //se il gioco è in play...
        if (GM.gameStatus == GameManager.GameStatus.gameRunning)
        {
            //si attivano i tutorial
            MoveTutorial();
            ShootingTutorial();
            BombTutorial();

            //i numeri corrispondenti al punteggio parziale, al punteggio totale e alle gemme raccolte si aggiornano su schermo
            XPText.text = pM.XPCount.ToString("000");
            totText.text = GM._score.ToString("000");
            gemsText.text = pM.gemCount.ToString();

            //le barre della vita del player, dei colpi e della vita del Boss si aggiornano
            ShootingBar.fillAmount = m.numColpi / m.numMaxColpi;
            HPBar.fillAmount = (float)pM.health / pM.maxHP;
            BossHPBar.fillAmount = (float)bM.health / bM.maxHP;
        }

        //se viene raggiunta la fine del livello...
        if (GM.gameStatus == GameManager.GameStatus.gameLevelEnd)
        {
            //il menù di fine livello si attiva e il punteggio totale si aggiorna
            levelEndCanvas.SetActive(true);
            pointsText.text = pM.score.ToString("000");
        }

        //se si raggiunge la fine del gioco...
        if (GM.gameStatus == GameManager.GameStatus.gameEnd)
        {
            //il punteggio totale si aggiorna
            TotPointsText.text = GM._score.ToString("000");
        }
    }

    //tutorial del movimento
    public void MoveTutorial()
    {
        //se si clicca uno qualsiasi di questi tasti, il tutorial di movimento si disattiva e si attiva quello dello shooting
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

    //tutorial dello shooting
    public void ShootingTutorial()
    {
        //se si clicca la barra spaziatrice, il tutorial svanisce
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutorialShoot.SetActive(false);
        }
    }

    //tutorial delle bombe
    public void BombTutorial()
    {
        //se il numero delle gemme è uguale a 0 e si raccoglie per la prima volta una gemma il tutorial si attiva
        if (numGems == 0)
        {
            if (pM.gemCount == 1)
            {
                //da questo momento in poi, non è più possibile riattivare il tutorial delle bombe, una volta chiuso
                tutorialBomb.SetActive(true);
                numGems++;
            }
        }

        //una volta lanciata la prima bomba, il tutorial si disattiva definitivamente
        else if (Input.GetKeyDown(KeyCode.E) && numGems == 1)
        {
            tutorialBomb.SetActive(false);
        }
    }

    //tasto Play dello StartCanvas
    public void Beginning()
    {
        startCanvas.SetActive(false);
        GM.gameStatus = GameManager.GameStatus.gameRunning;
    }

    //tasto NextLevel del LevelEndCanvas
    public void NextLevel()
    {
        levelEndCanvas.SetActive(false);
        GM.gameStatus = GameManager.GameStatus.gameRunning;

        //se il livello appena finito era il livello 1, si attiva la scena del livello 2 e viene mantenuto il punteggio totale
        if (pM.l2 == false)
        {
            levelEndCanvas.SetActive(false);
            SceneManager.LoadScene("Level_2", LoadSceneMode.Single);
            pM.ScoreRecord();
        }

        //se il livello appena finito era il livello 2, si attiva la scena del livello 3 e viene mantenuto il punteggio totale
        if (pM.l2 == true && pM.l3 == false)
        {
            levelEndCanvas.SetActive(false);
            SceneManager.LoadScene("Level_3", LoadSceneMode.Single);
            pM.ScoreRecord();
        }
    }

    //tasto Continue del PauseCanvas
    public void Continue()
    {
        GM.gameStatus = GameManager.GameStatus.gameRunning;
        pauseCanvas.SetActive(false);
    }

    //Tasto Restart del PauseCanvas, del DefeatCanvas e dell'EndCanvas
    public void Restart()
    {
        SceneManager.LoadScene("Level_1", LoadSceneMode.Single);

        //reset dei punteggi, della vita, delle gemme e dei livelli
        GM._score = 0;
        pM.gemCount = 0;
        pM.health = pM.maxHP;
        pM.l2 = false;
        pM.l3 = false;

        GM.gameStatus = GameManager.GameStatus.gameStart;
        pauseCanvas.SetActive(false);
    }

    //tasto Quit di tutti i Canvas
    public void Exit()
    {
        Application.Quit();
    }
}
