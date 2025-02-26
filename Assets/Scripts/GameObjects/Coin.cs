
using UnityEngine;
using UnityEngine.Events;

public class Coin : BasePowerup
{
    public AudioSource coinAudio;

    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.Coin;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && spawned)
        {
            ApplyPowerup(other.gameObject.GetComponent<PlayerMovement>());
            // then destroy powerup (optional)
            DestroyPowerup();
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        coinAudio.Play();
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        i.GetComponent<PlayerMovement>().Powerup(type);
    }
}
