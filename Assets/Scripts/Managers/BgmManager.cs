using UnityEngine;
using UnityEngine.Events;

public class BgmManager : MonoBehaviour
{
    public AudioSource gamebgm;
    public AudioSource pausebgm;

    void Awake()
    {
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameRestart.AddListener(GameStart);
        GameManager.instance.gameOver.AddListener(GameOver);
        GameManager.instance.gamePause.AddListener(GamePause);
        GameManager.instance.gameResume.AddListener(GameStart);
    }

    public void GameStart()
    {
        gamebgm.Play();
    }

    public void GamePause()
    {
        gamebgm.Stop();
        pausebgm.Play();
    }

    public void GameOver()
    {
        gamebgm.Stop();
    }
}