
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StarmanPowerup : BasePowerup
{
    public AudioSource starSound;

    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.StarMan;
        spawned = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            ApplyPowerup(col.gameObject.GetComponent<MonoBehaviour>());
            // then destroy powerup (optional)
            DestroyPowerup();

        }
        else if (col.gameObject.layer == 7 && spawned) // else if hitting Pipe, flip travel direction
        {
            goRight = !goRight;
            rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
    }

    public void PlayStarmanSound()
    {
        starSound.Play();
    }

    public override void ApplyPowerup(MonoBehaviour i)
    {
        BuffStateController mario;
        bool result = i.TryGetComponent<BuffStateController>(out mario);
        if (result)
        {
            mario.SetPowerup(this.powerupType);
        }
    }
}