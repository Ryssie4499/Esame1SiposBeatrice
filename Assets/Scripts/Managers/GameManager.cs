using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public delegate void Scoring();
    public static event Scoring Record;

    public int _score;

    //references
    UIManager UM;

    //prima dello Start il GameManager si assicura di essere in scena e di non essere distrutto nel passaggio tra una scena e l'altra
    private void Awake()
    {
        if (GameManager._instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UM = FindObjectOfType<UIManager>();
    }

    public enum GameStatus
    {
        gamePaused,
        gameRunning,
        gameLevelEnd,
        gameOver,
        gameEnd,
        gameStart
    }

    public GameStatus gameStatus = GameStatus.gameRunning;

    private void Update()
    {
        //ci si assicura che lo UIManager esista in scena e in caso contrario di ricercarlo
        if (UM == null)
            UM = FindObjectOfType<UIManager>();

        //se il gioco è in play e viene cliccato il tasto ESC, il gioco va in pausa e si attiva il PauseCanvas
        if (Input.GetKeyDown("escape") && gameStatus == GameStatus.gameRunning)
        {
            UM.pauseCanvas.SetActive(true);
            gameStatus = GameStatus.gamePaused;
        }

        //se il gioco è in pausa e viene cliccato il tasto ESC, il gioco torna in running
        else if (Input.GetKeyDown("escape") && gameStatus == GameStatus.gamePaused)
        {
            UM.Continue();
        }

        //in caso di sconfitta ci si assicura che si attivi il DefeatCanvas
        if (gameStatus == GameStatus.gameOver)
        {
            UM.defeatCanvas.SetActive(true);
        }
    }

    //mantenimento del punteggio tra un livello e l'altro
    public void EndLevel()
    {
        gameStatus = GameStatus.gameLevelEnd;
        Record?.Invoke();
    }

}
