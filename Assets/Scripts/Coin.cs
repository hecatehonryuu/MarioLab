
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public Animator coinAnimator;
    private BlockManager blockManager;
    private AudioSource coinAudio;
    private SpriteRenderer coinSprite;
    void Start()
    {
        blockManager = GameObject.Find("Blocks").GetComponent<BlockManager>();
        coinAudio = GetComponent<AudioSource>();
        coinSprite = GetComponent<SpriteRenderer>();
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
        blockManager.IncreaseScore(1);
    }

    public void GameRestart()
    {
        coinSprite.enabled = false;
    }
}
