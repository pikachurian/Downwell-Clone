using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MonoBehaviour
{
    public float speed = 2f;

    public float inFrontDistanceCheck = 0.5f;
    public float groundDistanceCheck = 0.5f;
    public LayerMask groundCheckMask;
    private bool isResting = true;

    private Animator animator;
    private Enemy enemyScript;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 direction = Vector2.right;
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
        UpdateGroundChecks();

        //move the worm
        transform.Translate(direction * speed * Time.deltaTime);

        //turn around the worm
        if (isWallInFront || !isGroundInFront)
        {
            transform.Rotate(0, 0, -90);
            direction = transform.right; // Update direction
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


    private void UpdateGroundChecks()
    {
        isWallInFront = Physics2D.Raycast(transform.position, Vector2.right * direction, inFrontDistanceCheck, groundCheckMask);
        isGroundInFront = Physics2D.Raycast(transform.position + Vector3.right * 1f * inFrontDistanceCheck, Vector2.down, groundDistanceCheck, groundCheckMask);
    }

    private void SetIsFacingRight(bool newIsFacingRight)
    {
        
    }
}
