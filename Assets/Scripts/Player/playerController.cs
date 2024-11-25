using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{

    public float runSpeed;
    public float jumpSpeed;
    public float jumpDampenPercent = 0.25f;
    public bool physicsComplete = false;

    private Animator myAnim;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;
    private bool isGround = false;

    //Shooting
    public GameObject bulletPrefab;
    public float shootForce = 1f;
    public bool canShoot = false;
    public float shootRateTime = 0.5f;
    private float shootRateTick = 0f;
    public Vector3 bulletSpawnOffset = Vector3.zero;
    public int ammo_max = 8;
    private int ammo_num;
    public Ammo_Bar ammo_Bar;

    //Health
    public HealthBar healthBar;
    public int hpMax = 4;
    private int hp = 1;

    //Stomping
    public float stompBoost = 5f;

    //Enemy Interactions
    //public bool isKnockbacked = false;
    public float immue_timer = 0f;
    public bool immune = false;
    public float knockbackForcement = 13f;
    public float immune_time = 0.5f;

    //public KeyCode shootInput;

    //Audio
    public AudioSource audioSource;
    public AudioClip introSound;
    private bool hasPlayedIntro = false;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip shootSound;
    public AudioClip reloadSound;



    // Start is called before the first frame update
    void Start()
    {
        SetAmmo(ammo_max);
        myAnim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();

        hp = hpMax;
        healthBar.SetMaxHealth(hpMax);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        if (!immune)
        { Running(); }
       
        Jump();
        CheckGrounded();
        Shoot();
        SwitchAnimation();
        Immue();
    }

    void CheckGrounded()
    {
        bool previousIsGround = isGround;
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("Platform"));

        //Check enemy;
        /*if (myFeet.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            myFeet.col

            canShoot = true;
            SetAmmo(ammo_max);
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, stompBoost);
        }*/

        if (isGround)
        {
            canShoot = false;

            if (ammo_num < ammo_max)
            {
                SetAmmo(ammo_max);
            }

            //Play intro music if first time touching ground
            if (hasPlayedIntro == false)
            {
                audioSource.PlayOneShot(introSound);
                hasPlayedIntro = true;
            }

            //Play land sound
            if (previousIsGround == false)
            {
                audioSource.PlayOneShot(landSound);
            }
        }

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
                audioSource.PlayOneShot(jumpSound);
            }
        }

        //When the player let's go of the jump button, reduce their Y velocity.
        //This creates a variable/charge jump.
        if (Input.GetButtonUp("Jump") || Input.GetKeyUp("space") || Input.GetKeyUp("up"))
        {
            if (myRigidbody.velocity.y > myRigidbody.velocity.y * jumpDampenPercent)
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
        if (ammo_num >= 1)
        {
            GameObject bulletInst = Instantiate(bulletPrefab);
            bulletInst.transform.position = transform.position + bulletSpawnOffset;
            SetAmmo(ammo_num -1);
            //Apply bullet "bounce"
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, shootForce);

            audioSource.PlayOneShot(shootSound);
        }
    }

    void SetAmmo( int amount )
    {
        ammo_num = amount;
        ammo_Bar.SetBulletGUI(ammo_num);

        //Play reload sound.
        if (amount == ammo_max)
        {
            audioSource.PlayOneShot(reloadSound);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Stomping
        //print(collision.gameObject.layer);
        if (collision.gameObject.layer == 9)//If on enemy layer
        {
            //print("Player stomped on enemy.");
            //---Stomp
            if (collision.transform.position.y < transform.position.y)
            {
                print(collision.gameObject.tag);
                switch(collision.gameObject.tag)
                {
                    case "Turtle":
                        collision.gameObject.GetComponent<Turtle>().TakeStompDamage(1);
                        break;

                    case "Bad_Bubble":
                        collision.gameObject.GetComponent<badBubble>().TakeStompDamage(1);
                        break;
                }

                canShoot = true;
                SetAmmo(ammo_max);
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, stompBoost);
            }

            //---Hit by Enemy
            if (collision.transform.position.y > transform.position.y && !immune)
            {
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
                Vector2 force = knockbackDirection * knockbackForcement;

                    switch (collision.gameObject.tag)
                    {
                        case "Bad_Bubble":
                            
                            myRigidbody.AddForce(force, ForceMode2D.Impulse);
                            immune = true;
                            break;
                        case "Turtle":

                            myRigidbody.AddForce(force, ForceMode2D.Impulse);
                            immune = true;
                            break;
                    }

                //Lose hp
                TakeDamage(1);
               
            }
        }
    }

   
    private void Immue()
    {
        if (immune)
        {
            immue_timer += Time.deltaTime;
        }
        if (immue_timer >= immune_time)
        {
            immune = false;
            immue_timer = 0;
        }

    }

    public void TakeDamage(int amount)
    {
        hp = Mathf.Max(hp - amount, 0);
        healthBar.SetHealth(hp);

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDrawGizmosSelected()
    {
        //Draw bullet spawn
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + bulletSpawnOffset, 0.5f);

    }
}
