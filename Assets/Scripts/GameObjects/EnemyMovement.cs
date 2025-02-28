using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Animator goombaAnimator;
    public AudioSource goombaAudio;
    public GameConstants gameConstants;
    public UnityEvent OnDamagePlayer;
    public UnityEvent<int> OnIncrementScore;
    float maxOffset;
    float enemyPatroltime;

    private int moveRight = -1;
    private float originalX;
    private bool alive = true;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;

    void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        // Set Constants
        maxOffset = gameConstants.goombaMaxOffset;
        enemyPatroltime = gameConstants.goombaPatrolTime;
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Transform marioTransform = col.gameObject.transform;
            if ((marioTransform.position.y - transform.position.y) > 0.2f)
            {
                // kill goomba
                alive = false;
                goombaAnimator.Play("goomba-die");
                goombaAudio.Play();
                // disable the collider
                GetComponent<Collider2D>().enabled = false;
                // disable the rigidbody
                enemyBody.bodyType = RigidbodyType2D.Static;
                // increment score
                OnIncrementScore.Invoke(1);
            }
            else if (alive)
            {
                // damage player
                OnDamagePlayer.Invoke();
            }
        }
        else if (col.gameObject.layer == 7) // else if hitting Pipe, flip travel direction
        {
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
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
