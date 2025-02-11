using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QBox : MonoBehaviour
{

    public Animator qboxAnimator;
    public Coin coin;
    private Rigidbody2D qboxBody;
    private Transform qboxTransform;
    private bool alive = true;

    void Start()
    {
        alive = true;
        qboxBody = GetComponent<Rigidbody2D>();
        qboxTransform = GetComponent<Transform>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (alive)
        {
            alive = false;
            qboxAnimator.Play("q-block-ded");
            coin.spawnCoin();
        }
    }

    void FixedUpdate()
    {
        if (!alive && qboxBody.linearVelocityY < 0 && qboxTransform.localPosition.y < 0.1f)
        {
            qboxTransform.localPosition = new Vector3(0, 0, 0);
            qboxBody.linearVelocity = new Vector2(0, 0);
            qboxBody.bodyType = RigidbodyType2D.Static;
        }
    }

    public void RestartButtonCallback(int input)
    {
        alive = true;
        qboxAnimator.Play("q-block-idle");
        qboxBody.bodyType = RigidbodyType2D.Dynamic;
    }
}
