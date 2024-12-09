using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MonoBehaviour
{
    public float speed = 0.25f;

    public float inFrontDistanceCheck = 0.2f;
    public float groundDistanceCheck = 0.2f;
    public LayerMask groundCheckMask;
    private Animator animator;
    private Enemy enemyScript;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 direction;
    public bool isWallInFront = false;
    public bool isGroundInFront = false;
    public string leftOrRight;

    public float turnTimer = 0.05f;
    public float tick = 0f;
    public bool hasTurned = false;
    // Start is called before the first frame update
    void Start()
    {

        enemyScript = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        DirectionDetection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        UpdateGroundChecks();

        //move the worm
        transform.Translate(direction * speed * Time.deltaTime);

        //turn around the worm

        TurnTimer();

    }

    private void Update()
    {
        TurnTimer();
    }

    void DirectionDetection()
    {
        if (this.transform.position.x > 0)
        {
            leftOrRight = "right";
            direction = Vector2.left;
        }
        if (this.transform.position.x < 0)
        {
            leftOrRight = "left";
            direction = Vector2.right;
        }
    }

    public void TakeStompDamage(int amount)
    {
        
    }

    public void TakeShotDamage(int amount)
    {
        enemyScript.TakeShotDamage(amount);
    }


    private void UpdateGroundChecks()
    {
        RaycastHit2D onGroundRay;
        RaycastHit2D infrontWallRay;
      
        
        onGroundRay = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), groundDistanceCheck, groundCheckMask);
        
        infrontWallRay = Physics2D.Raycast(transform.position, transform.TransformDirection(direction), inFrontDistanceCheck, groundCheckMask);

        if (!onGroundRay && !hasTurned)
        {
            NoGroundTurnCrawler();
            hasTurned = true;
            tick = 0f;
        }
        if (infrontWallRay && !hasTurned)
        {
            InfrontWallTurnCrawler();
            hasTurned = true;
            tick = 0f;
        }
    }

    private void NoGroundTurnCrawler()
    {
        if (leftOrRight == "left")
        {
            this.transform.eulerAngles += new Vector3(0, 0, -90);
        }
        if (leftOrRight == "right")
        {
            this.transform.eulerAngles += new Vector3(0, 0, 90);
        }
    }

    private void InfrontWallTurnCrawler()
    {
        if (leftOrRight == "left")
        {
            this.transform.eulerAngles += new Vector3(0, 0, 90);
        }
        if (leftOrRight == "right")
        {
            this.transform.eulerAngles += new Vector3(0, 0, -90);
        }
    }

    void TurnTimer()
    {
        if (hasTurned)
        {
            tick += Time.deltaTime;
            if (tick >= turnTimer)
            {
                hasTurned = false;
            }
        }
    }

}
