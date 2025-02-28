using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private Vector3[] scoreTextPosition = {
        new Vector3(-300, 200, 0),
        new Vector3(-50, -50, 0)
        };

    public GameObject scoreText;
    public GameObject hiScoreText;
    public GameObject restartButton;
    public GameObject pauseButton;
    public GameObject gameoverText;
    public GameObject gameOverPanel;
    public IntVariable gameScore;

    public void GameStart()
    {
        // hide gameover panel
        gameoverText.SetActive(false);
        gameOverPanel.SetActive(false);
        hiScoreText.SetActive(false);
        restartButton.SetActive(false);
        pauseButton.SetActive(true);
        pauseButton.GetComponent<PauseButtonController>().Reset();
        scoreText.transform.localPosition = scoreTextPosition[0];
    }

    public void SetScore()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + gameScore.Value.ToString();
    }


    public void GameOver()
    {
        gameoverText.SetActive(true);
        gameOverPanel.SetActive(true);
        hiScoreText.GetComponent<TextMeshProUGUI>().text = "TOP-" + gameScore.previousHighestValue.ToString("D6");
        hiScoreText.SetActive(true);
        pauseButton.SetActive(false);
        restartButton.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[1];
    }

    public void GamePause()
    {
        gameOverPanel.SetActive(true);
        restartButton.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[1];
    }
}
