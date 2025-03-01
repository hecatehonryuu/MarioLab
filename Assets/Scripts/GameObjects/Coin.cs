
using UnityEngine;
using UnityEngine.Events;

public class Coin : BasePowerup
{
    public AudioSource coinSound;
    public UnityEvent<int> onIncrementScore;

    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.Coin;
    }

    public void collectCoin()
    {
        onIncrementScore.Invoke(1);
        Destroy(gameObject);
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        onIncrementScore.Invoke(1);
    }

    public void PlayCoinSound()
    {
        coinSound.Play();
    }
}
