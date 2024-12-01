using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;
    public float stateTimeMin = 3f;
    public float stateTimeMax = 7f;

    public float inFrontDistanceCheck = 0.5f;
    public float groundDistanceCheck = 0.5f;
    public LayerMask groundCheckMask;

    private float stateTick = 3f;
    
    private bool isResting = true;
    
    private Animator animator;
    private Enemy enemyScript;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float facing = 1f;

    private bool isWallInFront = false;
    private bool isGroundInFront = false;

    // Start is called before the first frame update
    void Start()
    {
        
        enemyScript = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetIsFacingRight((Random.value < 0.5));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.velocity = Vector2.zero;

        UpdateGroundChecks();

        //move the worm
        rb.velocity = new Vector2(speed * facing, 0f);

        //turn around the worm
        if (isWallInFront || !isGroundInFront)
        {
            if (facing == 1f)
                SetIsFacingRight(false);
            else SetIsFacingRight(true);
        }

        /*  if (stateTick <= 0)
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
        enemyScript.TakeStompDamage(amount);
    }

    public void TakeShotDamage(int amount)
    {
        enemyScript.TakeShotDamage(amount);
    }

   /* private void SetIsResting(bool newIsResting)
    {
        stateTick = Random.Range(stateTimeMin, stateTimeMax);
        animator.SetBool("IsResting", newIsResting);
        isResting = newIsResting;

        if (isResting)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }
        else rb.isKinematic = false;
    }*/

    private void UpdateGroundChecks()
    {
        isWallInFront = Physics2D.Raycast(transform.position, Vector2.right * facing, inFrontDistanceCheck, groundCheckMask);
        isGroundInFront = Physics2D.Raycast(transform.position + Vector3.right * facing * inFrontDistanceCheck, Vector2.down, groundDistanceCheck, groundCheckMask);
    }

    private void SetIsFacingRight(bool newIsFacingRight)
    {
        if (newIsFacingRight)
        {
            facing = 1f;
            spriteRenderer.flipX = true;
        }
        else
        {
            facing = -1f;
            spriteRenderer.flipX = false;
        }
    }
}
