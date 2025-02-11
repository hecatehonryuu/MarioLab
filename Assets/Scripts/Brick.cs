using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Brick : MonoBehaviour
{
    public Coin coin;
    public float initvel;
    public bool spawnCoin = false;
    private bool alive = true;
    private Rigidbody2D brickBody;

    void Start()
    {
        alive = true;
        brickBody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        brickBody.linearVelocityY = initvel;
        if (spawnCoin && alive)
        {
            alive = false;
            coin.spawnCoin();
        }
    }
}
