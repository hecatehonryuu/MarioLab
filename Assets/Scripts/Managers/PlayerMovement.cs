using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);

    [Header("Mario Properties")]
    public float maxDistance;
    public Vector3 boxSize;

    [Header("References")]
    public LayerMask groundLayerMask;
    public LayerMask enemyLayerMask;
    public Animator marioAnimator;
    public AudioSource marioJumpAudio;
    public AudioSource marioDeathAudio;
    public AudioSource marioPowerupAudio;
    public GameConstants gameConstants;
    public BoolVariable marioFaceRight;
    public UnityEvent onGameOver;
    float deathImpulse;
    float upSpeed;
    float maxSpeed;
    float speed;

    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool jumpedState = false;
    private bool moving = false;

    // state
    [System.NonSerialized]
    public bool alive = true;


    void Start()
    {
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        // Set Constants
        deathImpulse = gameConstants.deathImpulse;
        upSpeed = gameConstants.upSpeed;
        maxSpeed = gameConstants.maxSpeed;
        speed = gameConstants.speed;
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
            Move(marioFaceRight.Value == true ? 1 : -1);
        }
    }

    public void DamageMario()
    {
        bool invincible = GetComponent<BuffStateController>().IsCurrentStateInvincible();
        if (invincible)
        {
            return;
        }
        GetComponent<MarioStateController>().SetPowerup(PowerupType.Damage);
    }

    public void FlipMarioSprite(int value)
    {

        if (value == -1 && marioFaceRight.Value)
        {
            marioFaceRight.SetValue(false);
            marioSprite.flipX = true;
            if (marioBody.linearVelocityX > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        else if (value == 1 && !marioFaceRight.Value)
        {
            marioFaceRight.SetValue(true);
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
        onGameOver.Invoke();
    }

    public void GameRestart()
    {
        marioBody.linearVelocity = Vector2.zero;
        marioBody.transform.position = startPosition;
        alive = true;
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
        marioJumpAudio.Play();
    }

    void PlayDeathSound()
    {
        marioDeathAudio.Play();
    }

    void PlayPowerupSound()
    {
        marioPowerupAudio.Play();
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

}
