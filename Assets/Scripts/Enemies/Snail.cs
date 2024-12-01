using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public float speed = 2f;

    public float upFrontDistanceCheck = 0.5f;
    public float groundDistanceCheck = 0.5f;
    public LayerMask groundCheckMask;

    private Animator animator;
    private Enemy enemyScript;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public float facing = 1f;

    private bool isWallUpFront = false;
    private bool isGroundUpFront = false;

    public float force;
    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetIsFacingUp((Random.value < 0.5));

        if (this.transform.position.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (this.transform.position.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.velocity = Vector2.zero;

        //UpdateGroundChecks();

        /* (isWallUpFront)
        {
            facing *= -1;
        }*/

        rb.velocity = new Vector2(0f, speed * facing);

        AttackWallTimer();

        if (this.transform.position.x < -4.75f)
        {
            transform.position = new Vector3(-4.75f, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.x > 3.7f)
        {
            transform.position = new Vector3(3.7f, this.transform.position.y, this.transform.position.z);
        }

        /*if (stateTick <= 0)
        {
            SetIsResting(!isResting);
        }
        else
        {
            stateTick -= Time.deltaTime;

            if (!isResting)
            {
                if (isWallInFront || !isGroundInFront)
                {
                    if (facing == 1f)
                        SetIsFacingRight(false);
                    else SetIsFacingRight(true);
                }

                rb.velocity = new Vector2(speed * facing, 0f);
            }
        }*/
    }

    public void TakeStompDamage(int amount)
    {
        //enemyScript.TakeStompDamage(amount);
    }

    public void TakeShotDamage(int amount)
    {
        enemyScript.TakeShotDamage(amount);
    }

    private void UpdateGroundChecks()
    {
        isWallUpFront = Physics2D.Raycast(transform.position, Vector2.up * facing, upFrontDistanceCheck, groundCheckMask);
        //isGroundUpFront = Physics2D.Raycast(transform.position + Vector3.up * facing * upFrontDistanceCheck, Vector2.down, groundDistanceCheck, groundCheckMask);
    }

    private void SetIsFacingUp(bool newIsFacingUp)
    {
        if (newIsFacingUp)
        {
            facing = 1f;
            spriteRenderer.flipY = false;
        }
        else
        {
            facing = -1f;
            spriteRenderer.flipY = true;
        }
    }

    private void AttachWall()
    {
        if (this.transform.position.x < 0)
        {
            rb.AddForce(Vector3.left * force, ForceMode2D.Impulse);
        }
        else if (this.transform.position.x > 0)
        {
            rb.AddForce(Vector3.right * force, ForceMode2D.Impulse);
        }
    }

    private void AttackWallTimer()
    {
        timer++;
        if(timer >= 7)
        {
            AttachWall();
            timer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if (collision.transform.position.y > this.transform.position.y && collision.transform.position.x >= this.transform.position.x)
            {
                if (facing == 1f)
                {
                    SetIsFacingUp(false);
                }
                else if (facing == -1f)
                {
                    SetIsFacingUp(true);
                }
            }
            else if (collision.transform.position.y < this.transform.position.y && collision.transform.position.x >= this.transform.position.x)
            {
                if (facing == 1f)
                {
                    SetIsFacingUp(true);
                }
                else if (facing == -1f)
                {
                    SetIsFacingUp(false);
                }
            }

            if (collision.transform.position.y > this.transform.position.y && collision.transform.position.x <= this.transform.position.x)
            {
                if (facing == 1f)
                {
                    SetIsFacingUp(false);
                }
                else if (facing == -1f)
                {
                    SetIsFacingUp(true);
                }
            }
            else if (collision.transform.position.y < this.transform.position.y && collision.transform.position.x <= this.transform.position.x)
            {
                if (facing == 1f)
                {
                    SetIsFacingUp(true);
                }
                else if (facing == -1f)
                {
                    SetIsFacingUp(false);
                }
            }
        }
    }
}
