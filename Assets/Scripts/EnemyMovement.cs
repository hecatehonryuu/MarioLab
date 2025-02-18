using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float maxOffset = 5.0f;
    public float enemyPatroltime = 2.0f;
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Animator goombaAnimator;
    public AudioSource goombaAudio;
    private int moveRight = -1;
    private float originalX;
    private bool alive = true;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Transform marioTransform = other.gameObject.transform;
            if (Mathf.Abs(transform.position.x - marioTransform.position.x) < 0.7f && (marioTransform.position.y - transform.position.y) > 0.5f)
            {
                // kill goomba
                alive = false;
                goombaAnimator.Play("goomba-die");
                goombaAudio.Play();
                // disable the collider
                GetComponent<Collider2D>().enabled = false;
                // disable the rigidbody
                enemyBody.bodyType = RigidbodyType2D.Static;
            }
        }
    }

    void Update()
    {
        if (alive)
        {
            if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
            {// move goomba
                Movegoomba();
            }
            else
            {
                // change direction
                moveRight *= -1;
                ComputeVelocity();
                Movegoomba();
            }
        }
    }
    void ComputeVelocity()
    {
        velocity = new Vector2(moveRight * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    public void GoombaDead()
    {
        gameObject.SetActive(false);
    }

    public void GameRestart()
    {
        gameObject.SetActive(true);
        alive = true;
        GetComponent<Collider2D>().enabled = true;
        enemyBody.bodyType = RigidbodyType2D.Kinematic;
        goombaAnimator.Play("goomba-walk");
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
    }
}
