using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QBox : MonoBehaviour
{

    public Animator qboxAnimator;
    public Coin coin;
    public float initvel = 10;
    private bool alive = true;
    private Rigidbody2D qboxBody;

    void Awake()
    {
        qboxBody = GetComponent<Rigidbody2D>();
        GameManager.instance.gameStart.AddListener(GameRestart);
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }
    void Start()
    {
        alive = true;
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
        qboxBody.bodyType = RigidbodyType2D.Dynamic;
        qboxBody.linearVelocity = new Vector2(0, 0);
        transform.localPosition = new Vector3(0, 0, 0);
        qboxAnimator.Play("q-block-idle");
        coin.GameRestart();
    }

}
