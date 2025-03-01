using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Brick : MonoBehaviour
{
    public float initvel = 10;
    public bool spawnCoin = false;
    private bool alive = true;
    public GameObject coinPrefab;
    private Rigidbody2D brickBody;

    void Start()
    {
        brickBody = GetComponent<Rigidbody2D>();
        alive = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        brickBody.linearVelocityY = initvel;
        if (spawnCoin && alive)
        {
            alive = false;
            Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity, transform);
        }
    }


    public void GameRestart()
    {
        alive = true;
        brickBody.linearVelocity = new Vector2(0, 0);
        transform.localPosition = new Vector3(0, 0, 0);
    }
}
