using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 startCameraPosition = new Vector3(4.0f, 0.0f, -20f);

    [Header("Mario Properties")]
    public float speed = 10;
    public float maxSpeed = 20;
    public float upSpeed = 1;
    public float maxDistance;
    public float deathImpulse = 15;
    public Vector3 boxSize;

    [Header("References")]
    public LayerMask groundLayerMask;
    public LayerMask enemyLayerMask;
    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioSource marioDeathAudio;
    public AudioSource coinAudio;
    public AudioSource shroomAudio;
    public Transform gameCamera;

    private GameManager gameManager;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private bool jumpedState = false;
    private bool moving = false;

    // state
    [System.NonSerialized]
    public bool alive = true;


    void Awake()
    {
        gameManager = GameManager.instance;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        // subscribe to Game Restart event
        gameManager.gameStart.AddListener(GameRestart);
        gameManager.gameRestart.AddListener(GameRestart);
    }


    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        // update animator state
        marioAnimator.SetBool("onGround", true);
    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.linearVelocityX));
    }

    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);
    void OnCollisionEnter2D(Collision2D col)
    {

        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & onGroundCheck())
        {
            // update animator state
            marioAnimator.SetBool("onGround", true);
            jumpedState = false;
        }
    }


    // FixedUpdate is called 50 times a second
    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            Transform enemyTransform = other.gameObject.transform;
            if (Mathf.Abs(transform.position.x - enemyTransform.position.x) < 0.7f && (transform.position.y - enemyTransform.position.y) > 0.5f)
            {
                marioBody.AddForce(Vector2.up * upSpeed / 2, ForceMode2D.Impulse);
                gameManager.IncreaseScore(1);
            }
            else
            {
                // play death animation
                marioAnimator.Play("mario-die");
                marioDeathAudio.Play();
                alive = false;
            }
        }
    }

    public void FlipMarioSprite(int value)
    {

        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.linearVelocityX > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.linearVelocityX < -0.1f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    private void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.linearVelocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    public void Jump()
    {
        if (alive && onGroundCheck())
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", false);

        }
    }

    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
    }

    public void GameOver()
    {
        gameManager.GameOver();
    }

    public void GameRestart()
    {
        // reset position
        marioBody.linearVelocity = Vector2.zero;
        marioBody.transform.position = startPosition;
        // reset sprite direction
        // faceRightState = true;
        // marioSprite.flipX = false;
        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;
        // reset camera position
        gameCamera.position = startCameraPosition;

    }

    private bool onGroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, groundLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    public void Powerup(PowerupType type)
    {
        if (type == PowerupType.Coin)
        {
            coinAudio.Play();
            gameManager.IncreaseScore(1);
        }
        if (type == PowerupType.MagicMushroom)
        {
            shroomAudio.Play();
        }
    }

}
