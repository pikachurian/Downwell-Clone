using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float runSpeed;
    public float jumpSpeed;
    public float jumpDampenPercent = 0.25f;
    public bool physicsComplete = false;

    private Animator myAnim;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;
    private bool isGround;

    //Shooting
    public GameObject bulletPrefab;
    public float shootForce = 1f;
    public bool canShoot = false;
    public float shootRateTime = 0.5f;
    private float shootRateTick = 0f;
    public Vector3 bulletSpawnOffset = Vector3.zero;
    //public KeyCode shootInput;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Running();
        Jump();
        CheckGrounded();
        Shoot();
        SwitchAnimation();
    }

    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("Platform"));

        if (isGround)
            canShoot = false;

        myAnim.SetFloat("Vertical Speed", myRigidbody.velocity.y);
    }

    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Running()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetFloat("Horizontal Speed", Mathf.Abs(myRigidbody.velocity.x));
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown("space") || Input.GetKeyDown("up"))
        {
            if (isGround)
            {
                //myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
            }
        }

        //When the player let's go of the jump button, reduce their Y velocity.
        //This creates a variable/charge jump.
        if (Input.GetButtonUp("Jump") || Input.GetKeyUp("space") || Input.GetKeyUp("up"))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y * jumpDampenPercent);
        }
    }

    void Shoot()
    {
        if (Input.GetButtonUp("Jump") || Input.GetKeyUp("space") || Input.GetKeyUp("up"))
        {
            if (!isGround && canShoot == false)
            {
                canShoot = true;
                shootRateTick = 0f;
                print("You can shoot, again!");
            }
        }
        
        if (Input.GetButton("Jump") || Input.GetKey("space") || Input.GetKey("up"))
        {
            if (!isGround && canShoot)
            {
                if (shootRateTick <= 0f)
                {
                    FireBullet();
                    shootRateTick = shootRateTime;
                }
                else shootRateTick -= Time.deltaTime;
            }
        }
    }

    void FireBullet()
    {
        //Spawn bullet
        GameObject bulletInst = Instantiate(bulletPrefab);
        bulletInst.transform.position = transform.position + bulletSpawnOffset;

        //Apply bullet "bounce"
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, shootForce);
    }

    void SwitchAnimation()
    {
        if (myRigidbody.velocity.y < 0.0f)
        {
            //myAnim.SetBool("Jump", false);
            //myAnim.SetBool("Fall", true);
        }
        else if (isGround)
        {
            //myAnim.SetBool("Fall", false);
            //myAnim.SetBool("Idle", true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Draw bullet spawn
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + bulletSpawnOffset, 0.5f);

    }
}
