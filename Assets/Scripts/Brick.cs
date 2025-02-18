using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Brick : MonoBehaviour
{
    public GameObject brick;
    public Coin coin;
    public float initvel;
    public bool spawnCoin = false;
    private bool alive = true;
    private Transform brickTransform;
    private Rigidbody2D brickBody;

    void Start()
    {
        alive = true;
        brickBody = brick.GetComponent<Rigidbody2D>();
        brickTransform = brick.transform;
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


    public void GameRestart()
    {
        alive = true;
        brickBody.linearVelocity = new Vector2(0, 0);
        brickTransform.localPosition = new Vector3(0, 0, 0);
        if (spawnCoin)
        {
            coin.GameRestart();
        }
    }
}
