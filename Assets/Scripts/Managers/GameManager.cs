using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public delegate void Scoring();
    public static event Scoring Record;
    public int _score;
    
    UIManager UM;
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
        if (UM == null)
            UM = FindObjectOfType<UIManager>();

        if (Input.GetKeyDown("escape") && gameStatus == GameStatus.gameRunning)
        {
            UM.pauseCanvas.SetActive(true);
            gameStatus = GameStatus.gamePaused;
        }
        else if (Input.GetKeyDown("escape") && gameStatus == GameStatus.gamePaused)
            UM.Continue();
        if(gameStatus == GameStatus.gameOver)
        {
            UM.startCanvas.SetActive(false);
            UM.defeatCanvas.SetActive(true);
        }
    }
   
    public void EndLevel()
    {
        gameStatus = GameStatus.gameLevelEnd;
        Record?.Invoke();
    }

}
