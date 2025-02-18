
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public Animator coinAnimator;
    public UnityEvent<int> coinEvent;
    private AudioSource coinAudio;
    private SpriteRenderer coinSprite;
    void Start()
    {
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
        coinEvent.Invoke(1);
    }

    public void GameRestart()
    {
        coinSprite.enabled = false;
    }
}
