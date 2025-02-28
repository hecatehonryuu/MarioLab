using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // events
    public UnityEvent onGameStart;
    public UnityEvent OnUpdateScore;
    public IntVariable gameScore;

    void Start()
    {
        onGameStart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void GameRestart()
    {
        // reset score
        gameScore.Value = 0;
        OnUpdateScore.Invoke();
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(increment);
        Debug.Log(gameScore.Value);
        OnUpdateScore.Invoke();
    }


    public void GameOver()
    {
        Time.timeScale = 0.0f;
    }
}