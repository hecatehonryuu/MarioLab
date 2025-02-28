using UnityEngine;
using UnityEngine.Events;

public class BgmManager : MonoBehaviour
{
    public AudioSource gamebgm;
    public AudioSource pausebgm;

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