using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public float speed = 1f;

    public float upFrontDistanceCheck = 0.5f;
    public float groundDistanceCheck = 0.5f;
    public LayerMask groundCheckMask;

    private Animator animator;
    private Enemy enemyScript;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public float facing = 1f;
    public string leftOrRight;

    private Vector2 direction;
    private bool isWallUpFront = false;
    private bool isGroundUpFront = false;

    public float force;
    public float timer = 0f;
    public bool hasTurned;
    public float tick;
    public float turnTimer;
    public TimeStopManager timeStopManager;
    public float awakeDistance;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        DirectionDetection();

        player = GameObject.FindGameObjectWithTag("Player").gameObject;

        timeStopManager = GameObject.FindGameObjectWithTag("TimeStopManager").GetComponent<TimeStopManager>();


    }

    void DirectionDetection()
    {
        if (this.transform.position.x > 0)
        {
            leftOrRight = "right";
            direction = Vector2.right;
        }
        if (this.transform.position.x < 0)
        {
            leftOrRight = "left";
            direction = Vector2.left;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (timeStopManager.GetIsTimePaused())
        {
            speed = 0F;
        }
        else if (!timeStopManager.GetIsTimePaused() && distanceToPlayer <= awakeDistance)
        {
            speed = 1f;
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        //rb.velocity = new Vector2(0f, speed * facing);

        UpdateGroundChecks();

        TurnTimer();
    }

    public void TakeStompDamage(int amount)
    {
        //enemyScript.TakeStompDamage(amount);
    }

    public void TakeShotDamage(int amount)
    {
        enemyScript.TakeShotDamage(amount);
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


    private void UpdateGroundChecks()
    {
        RaycastHit2D onGroundRay;
        RaycastHit2D infrontWallRay;


        onGroundRay = Physics2D.Raycast(transform.position, transform.TransformDirection(direction), groundDistanceCheck, groundCheckMask);

        infrontWallRay = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), upFrontDistanceCheck, groundCheckMask);

        if (!onGroundRay && !hasTurned)
        {
           
            this.transform.eulerAngles += new Vector3(180, 0, 0);
            tick = 0f;
            hasTurned = true;
        }
        if (infrontWallRay && !hasTurned)
        {
           
            this.transform.eulerAngles += new Vector3(180, 0, 0);
            tick = 0f;
            hasTurned = true;
        }
    }

    //private void SetIsFacingUp(bool newIsFacingUp)
    //{
    //    if (newIsFacingUp)
    //    {
    //        facing = 1f;
    //        spriteRenderer.flipY = false;
    //    }
    //    else
    //    {
    //        facing = -1f;
    //        spriteRenderer.flipY = true;
    //    }
    //}

    //private void AttachWall()
    //{
    //    if (this.transform.position.x < 0)
    //    {
    //        rb.AddForce(Vector3.left * force, ForceMode2D.Impulse);
    //    }
    //    else if (this.transform.position.x > 0)
    //    {
    //        rb.AddForce(Vector3.right * force, ForceMode2D.Impulse);
    //    }
    //}

    //private void LeaveWall()
    //{
    //    if (this.transform.position.x < 0)
    //    {

    //        rb.AddForce(Vector3.right * force, ForceMode2D.Impulse);
    //    }
    //    else if (this.transform.position.x > 0)
    //    {
    //        rb.AddForce(Vector3.left * force, ForceMode2D.Impulse);
    //    }
    //}

    //public void SetFace()
    //{
    //    if (facing == 1f)
    //    {
    //        SetIsFacingUp(false);
    //    }
    //    else if (facing == -1f)
    //    {
    //        SetIsFacingUp(true);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
