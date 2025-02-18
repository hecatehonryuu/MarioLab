using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QBox : MonoBehaviour
{

    public Animator qboxAnimator;
    public Coin coin;
    public GameObject qbox;
    public BlockManager blockManager;
    public float initvel;
    private bool alive = true;
    private Rigidbody2D qboxBody;
    private Transform qboxTransform;

    void Start()
    {
        alive = true;
        qboxBody = qbox.GetComponent<Rigidbody2D>();
        qboxTransform = qbox.transform;
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
        if (!alive && qboxBody.linearVelocityY < 0 && qboxTransform.localPosition.y < 0.1f)
        {
            qboxTransform.localPosition = new Vector3(0, 0, 0);
            qboxBody.linearVelocity = new Vector2(0, 0);
            qboxBody.bodyType = RigidbodyType2D.Static;
        }
    }

    public void IncreaseScore(int val)
    {
        blockManager.IncreaseScore(val);
    }

    public void GameRestart()
    {
        alive = true;
        qboxBody.bodyType = RigidbodyType2D.Dynamic;
        qboxBody.linearVelocity = new Vector2(0, 0);
        qboxTransform.localPosition = new Vector3(0, 0, 0);
        qboxAnimator.Play("q-block-idle");
        coin.GameRestart();
    }

}
