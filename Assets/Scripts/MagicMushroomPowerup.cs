
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroomPowerup : BasePowerup
{
    public AudioSource shroomAudio;

    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.MagicMushroom;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            ApplyPowerup(col.gameObject.GetComponent<PlayerMovement>());
            // then destroy powerup (optional)
            DestroyPowerup();

        }
        else if (col.gameObject.layer == 7 || col.gameObject.layer == 3) // else if hitting Ground/Pipe, flip travel direction
        {
            if (spawned)
            {
                if (col.gameObject.layer == 7)
                {
                    goRight = !goRight;
                }
                rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);
                rigidBody.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            }
        }
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        shroomAudio.Play();
    }

    void startmove()
    {
        spawned = true;
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
        rigidBody.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        i.GetComponent<PlayerMovement>().Powerup(type);
    }
}