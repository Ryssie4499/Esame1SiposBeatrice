using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    string scoreKey = "Score";
    public int CurrentScore { get; set; }
    private void Start()
    {
        CurrentScore = PlayerPrefs.GetInt(scoreKey);
    }
    
    public void UpdateScore(int score)
    {
        PlayerPrefs.SetInt(scoreKey, score);
    }
}
