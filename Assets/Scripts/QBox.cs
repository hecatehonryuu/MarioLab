using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QBox : MonoBehaviour
{

    public Animator qboxAnimator;
    public Coin coin;
    public float initvel;
    private Rigidbody2D qboxBody;
    private bool alive = true;

    void Start()
    {
        alive = true;
        qboxBody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (alive)
        {
            alive = false;
            qboxBody.linearVelocityY = initvel;
            qboxAnimator.Play("q-block-ded");
            coin.spawnCoin();
        }
    }

    void FixedUpdate()
    {
        if (!alive && qboxBody.linearVelocityY < 0 && transform.localPosition.y < 0.1f)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            qboxBody.linearVelocity = new Vector2(0, 0);
            qboxBody.bodyType = RigidbodyType2D.Static;
        }
    }

    public void GameRestart()
    {
        alive = true;
        qboxBody.linearVelocity = new Vector2(0, 0);
        qboxBody.bodyType = RigidbodyType2D.Dynamic;
        transform.localPosition = new Vector3(0, 0, 0);
    }

}
