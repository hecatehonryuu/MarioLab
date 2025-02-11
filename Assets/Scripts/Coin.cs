
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Animator coinAnimator;
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
    public void playCoinSound()
    {
        coinAudio.Play();
        coinAnimator.Play("coin-idle");
        coinSprite.enabled = false;
    }
}
