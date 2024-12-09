using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public float speed = 2f;
    public float stateTimeMin = 3f;
    public float stateTimeMax = 7f;
    public float force = 3f;

    public float inFrontDistanceCheck = 0.5f;
    public float groundDistanceCheck = 0.5f;
    public LayerMask groundCheckMask;

    private float stateTick = 3f;

    private Enemy enemyScript;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float facing = 1f;

    private bool isWallInFront = false;
    private bool isGroundInFront = false;

    public TimePause timePause;

    public GameObject player;
    private BoxCollider2D frogFeet;
    public bool isGround = false;

    public float timer = 180f;
    private float timeCount;


    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetIsFacingRight((Random.value < 0.5));
        frogFeet = GetComponent<BoxCollider2D>();
        timeCount = timer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timePause.timeStop)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if (!timePause.timeStop)
        {
            CheckGrounded();
            if (isGround)
            {
                timeCount--;
                if (this.transform.position.x >= player.transform.position.x)
                {
                    SetIsFacingRight(false);
                }
                else if (this.transform.position.x < player.transform.position.x)
                {
                    SetIsFacingRight(true);
                }
                if (timeCount < 0)
                {
                    Jump();
                }
            }
        }
    }

    public void TakeStompDamage(int amount)
    {
        enemyScript.TakeStompDamage(amount);
    }

    public void TakeShotDamage(int amount)
    {
        enemyScript.TakeShotDamage(amount);
    }

    void CheckGrounded()
    {
        bool previousIsGround = isGround;
        isGround = frogFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) || frogFeet.IsTouchingLayers(LayerMask.GetMask("Platform"));
    }

    private void SetIsFacingRight(bool newIsFacingRight)
    {
        if (newIsFacingRight)
        {
            facing = 1f;
            spriteRenderer.flipX = false;
        }
        else
        {
            facing = -1f;
            spriteRenderer.flipX = true;
        }
    }

    private void Jump()
    {
        if (this.transform.position.x >= player.transform.position.x)
        {
            Vector2 forceDirection = new Vector2(-1, 1).normalized;

            // Apply force in that direction
            rb.AddForce(forceDirection * force, ForceMode2D.Impulse);
        }
        else if (this.transform.position.x < player.transform.position.x)
        {
            Vector2 forceDirection = new Vector2(1, 1).normalized;

            // Apply force in that direction
            rb.AddForce(forceDirection * force, ForceMode2D.Impulse);
        }
        timeCount = timer;
    }
}
