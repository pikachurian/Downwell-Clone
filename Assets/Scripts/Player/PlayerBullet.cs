using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;

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
                        collision.gameObject.GetComponent<badBubble>().TakeShotDamage(1);
                        break;
                    case "worm":
                        collision.gameObject.GetComponent<Worm>().TakeShotDamage(1);
                        break;
                    case "Bat":
                        collision.gameObject.GetComponent<Bat>().TakeShotDamage(1);
                        break;
                }
                animator.SetTrigger("HitGround");
                break;
        }
    }

    public void Despawn()
    {
        Destroy(this.gameObject);
    }
}
