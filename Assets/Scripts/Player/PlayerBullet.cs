using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public int damage = 1;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(0f, -speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Ground":
                print("Bullet hit ground");
                animator.SetTrigger("HitGround");
                break;
            case "Enemy":
                animator.SetTrigger("HitGround");
                break;
        }
        switch (collision.gameObject.layer)
        {
            case 9://enemy layer
                switch (collision.gameObject.tag)
                {
                    case "Bad_Bubble":
                        collision.gameObject.GetComponent<badBubble>().TakeShotDamage(damage);
                        break;
                    case "worm":
                        collision.gameObject.GetComponent<Worm>().TakeShotDamage(damage);
                        break;
                    case "Bat":
                        collision.gameObject.GetComponent<Bat>().TakeShotDamage(damage);
                        break;
                    case "snail":
                        collision.gameObject.GetComponent<Snail>().TakeShotDamage(damage);
                        break;
                    case "crawler":
                        collision.gameObject.GetComponent<Crawler>().TakeShotDamage(damage);
                        break;
                   
                }
                animator.SetTrigger("HitGround");
                break;
        }

        if (collision.gameObject.tag == "gempile")
        {
            collision.gameObject.GetComponent<Gempile>().TakeShotDamage(damage);
            collision.gameObject.GetComponent<Gempile>().SpwanGem();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            collision.gameObject.GetComponent<Obstacle>().TakeShotDamage(damage);
        }
       
    }

    public void Despawn()
    {
        Destroy(this.gameObject);
    }
}
