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

    public TimePause timePause;

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
            
        }
        else if (this.transform.position.x > 0)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * -1,this.transform.localScale.y,0);
        }
       

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
            rb.velocity = new Vector2(0f, speed * facing);
        }
        //rb.velocity = new Vector2(0f, speed * facing);
    }

    public void TakeStompDamage(int amount)
    {
        //enemyScript.TakeStompDamage(amount);
    }

    public void TakeShotDamage(int amount)
    {
        enemyScript.TakeShotDamage(amount);
    }

    bool IsLeftUpClear(float distance, LayerMask wallMask)
    {

        // Perform the raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(-1,1,0), distance, wallMask);

        // Check if the raycast hit anything
        if (hit.collider == null)
        {
            // No wall was detected in the left-up direction
            return true;
        }
        else
        {
            // Wall was detected
            return false;
        }
    }

    bool IsRightUpClear(float distance, LayerMask wallMask)
    {

        // Perform the raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position,  new Vector3(1, 1, 0), distance, wallMask);

        // Check if the raycast hit anything
        if (hit.collider == null)
        {
            // No wall was detected in the left-up direction
            return true;
        }
        else
        {
            // Wall was detected
            return false;
        }
    }


    private void UpdateGroundChecks()
    {
     //   isWallUpFront = Physics2D.Raycast(transform.position, Vector2.up * facing, upFrontDistanceCheck, groundCheckMask);
        //isGroundUpFront = Physics2D.Raycast(transform.position, Vector2.up * facing, groundDistanceCheck, groundCheckMask);

        if (this.transform.position.x > 0)
        {
            isGroundUpFront = IsRightUpClear(groundDistanceCheck,groundCheckMask);
        }
        if (this.transform.position.x < 0)
        {
            isGroundUpFront = IsLeftUpClear(groundDistanceCheck, groundCheckMask);
        }
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

    private void LeaveWall()
    {
        if (this.transform.position.x < 0)
        {

            rb.AddForce(Vector3.right * force, ForceMode2D.Impulse);
        }
        else if (this.transform.position.x > 0)
        {
            rb.AddForce(Vector3.left * force, ForceMode2D.Impulse);
        }
    }

    public void SetFace()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            LeaveWall();

            if (collision.transform.position.y > this.transform.position.y && collision.transform.position.x >= this.transform.position.x)
            {
                SetFace();
            }
            else if (collision.transform.position.y < this.transform.position.y && collision.transform.position.x >= this.transform.position.x)
            {
                SetFace();
            }

            if (collision.transform.position.y > this.transform.position.y && collision.transform.position.x <= this.transform.position.x)
            {
                SetFace();
            }
            else if (collision.transform.position.y < this.transform.position.y && collision.transform.position.x <= this.transform.position.x)
            {
                SetFace();
            }
        }
    }
}
