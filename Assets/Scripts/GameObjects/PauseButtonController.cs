using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
    private bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;
    private Image image;
    // Start is called before the first frame update

    void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {
        image.sprite = pauseIcon;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClick()
    {
        Time.timeScale = isPaused ? 1.0f : 0.0f;
        isPaused = !isPaused;
        if (isPaused)
        {
            image.sprite = playIcon;
            GameManager.instance.GamePause();
        }
        else
        {
            image.sprite = pauseIcon;
            GameManager.instance.GameResume();
        }
    }

    public void Reset()
    {
        isPaused = false;
        image.sprite = pauseIcon;
    }
}
