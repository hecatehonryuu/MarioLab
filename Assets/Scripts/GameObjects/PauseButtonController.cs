using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
    private bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;
    public UnityEvent onGamePause;
    public UnityEvent onGameResume;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = pauseIcon;
        isPaused = false;
    }

    public void ButtonClick()
    {
        Time.timeScale = isPaused ? 1.0f : 0.0f;
        isPaused = !isPaused;
        if (isPaused)
        {
            image.sprite = playIcon;
            onGamePause.Invoke();
        }
        else
        {
            image.sprite = pauseIcon;
            onGameResume.Invoke();
        }
    }

    public void Reset()
    {
        isPaused = false;
        image.sprite = pauseIcon;
    }
}
