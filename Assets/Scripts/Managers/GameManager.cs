using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public delegate void Scoring();
    public static event Scoring Record;
    PlayerMovement pM;

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
        pM = FindObjectOfType<PlayerMovement>();
    }
    public enum GameStatus
    {
        gamePaused,
        gameRunning,
        gameEnd,
        gameStart
    }
    public GameStatus gameStatus = GameStatus.gameRunning;

    void Update()
    {
        if (Input.GetKeyDown("escape") && gameStatus == GameStatus.gameRunning)
        {
            gameStatus = GameStatus.gamePaused;
            //UM.pauseMenu.SetActive(true);
        }

    }
    public void EndLevel()
    {
        gameStatus = GameStatus.gameEnd;
        Record?.Invoke();
    }
    public void Exit()
    {
        Application.Quit();
    }
   
}
