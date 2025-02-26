
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public Animator coinAnimator;
    private AudioSource coinAudio;
    private SpriteRenderer coinSprite;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.instance;
        coinAudio = GetComponent<AudioSource>();
        coinSprite = GetComponent<SpriteRenderer>();
        gameManager.gameStart.AddListener(GameRestart);
        gameManager.gameRestart.AddListener(GameRestart);
    }
    void Start()
    {
        coinSprite.enabled = false;
    }

    public void spawnCoin()
    {
        coinSprite.enabled = true;
        coinAnimator.Play("coin-jump");

    }
    public void collectCoin()
    {
        coinAudio.Play();
        coinAnimator.Play("coin-idle");
        coinSprite.enabled = false;
        gameManager.IncreaseScore(1);
    }

    public void GameRestart()
    {
        coinSprite.enabled = false;
    }
}
