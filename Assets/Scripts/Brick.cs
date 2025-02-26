using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Brick : MonoBehaviour
{
    public Coin coin;
    public float initvel = 10;
    public bool spawnCoin = false;
    private bool alive = true;
    private Rigidbody2D brickBody;

    void Awake()
    {
        brickBody = GetComponent<Rigidbody2D>();
        GameManager.instance.gameStart.AddListener(GameRestart);
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }
    void Start()
    {
        alive = true;
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
        transform.localPosition = new Vector3(0, 0, 0);
        if (spawnCoin)
        {
            coin.GameRestart();
        }
    }
}
