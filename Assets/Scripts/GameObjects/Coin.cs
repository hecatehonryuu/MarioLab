
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && spawned)
        {
            ApplyPowerup(other.GetComponent<MonoBehaviour>());
            // then destroy powerup (optional)
            coinSound.Play();
            DestroyPowerup();
        }
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
