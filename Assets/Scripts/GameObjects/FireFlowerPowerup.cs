
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireFlowerPowerup : BasePowerup
{
    public AudioSource flowerSound;

    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.FireFlower;
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
    }

    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
    }

    public void PlayFlowerSound()
    {
        flowerSound.Play();
    }
}