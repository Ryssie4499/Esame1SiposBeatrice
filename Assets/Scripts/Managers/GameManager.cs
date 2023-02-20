using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public delegate void Scoring();
    public static event Scoring Record;
    PlayerMovement pM;
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
        //gameStatus = GameStatus.gameStart;
        pM = FindObjectOfType<PlayerMovement>();
        UM = FindObjectOfType<UIManager>();
    }
    public enum GameStatus
    {
        gamePaused,
        gameRunning,
        gameLevelEnd,
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
            Debug.Log("Pausaaaa");
            UM.pauseCanvas.SetActive(true);
            gameStatus = GameStatus.gamePaused;
        }
        else if (Input.GetKeyDown("escape") && gameStatus == GameStatus.gamePaused)
            UM.Continue();
    }

    
        

    public void EndLevel()
    {
        gameStatus = GameStatus.gameLevelEnd;
        Record?.Invoke();
    }
    public void Exit()
    {
        Application.Quit();
    }
   
}
