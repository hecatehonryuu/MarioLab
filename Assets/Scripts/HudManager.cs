using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private Vector3[] scoreTextPosition = {
        new Vector3(-300, 200, 0),
        new Vector3(-50, 0, 0)
        };
    private Vector3[] restartButtonPosition = {
        new Vector3(375, 200, 0),
        new Vector3(0, -50, 0)
    };

    public GameObject scoreText;
    public GameObject restartButton;
    public GameObject gameoverText;

    public GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        // hide gameover panel
        gameoverText.SetActive(false);
        gameOverPanel.SetActive(false);
        scoreText.transform.localPosition = scoreTextPosition[0];
        restartButton.transform.localPosition = restartButtonPosition[0];
    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }


    public void GameOver()
    {
        gameoverText.SetActive(true);
        gameOverPanel.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[1];
        restartButton.transform.localPosition = restartButtonPosition[1];
    }
}
