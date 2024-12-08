using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Bat : MonoBehaviour
{
    private enum State
    {
        idle,
        chase,
        death
    }
    private State currentState = State.idle;
    public float speed;
    private GameObject player;
    private Vector2 direction;
    public Collider2D bat;
    public Collider2D playerCollider;
    public Enemy enemy;
    private Rigidbody2D rb;
    public float shotKnockbackValue = 2f;

    public TimePause timePause;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timePause.timeStop)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if (!timePause.timeStop)
        {
            switch (currentState)
            {
                case State.idle:

                    break;

                case State.chase:
                    chase();
                    break;

                case State.death:
                    Destroy(this.gameObject);
                    break;
            }

            if (player.transform.position.y <= this.transform.position.y)
            {
                currentState = State.chase;
            }
        }

        //Physics2D.IgnoreCollision(bat, playerCollider, false);
    }

    private void chase()
    {
        direction = (player.transform.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.transform.position.y > this.transform.position.y && collision.gameObject.tag == "Player")
        {
            currentState = State.death;
        }

        //Physics2D.IgnoreCollision(bat, playerCollider, false);
    }

    public void TakeStompDamage(int amount)
    {
        enemy.TakeStompDamage(amount);
    }

    public void TakeShotDamage(int amount)
    {
        enemy.TakeShotDamage(amount);
        rb.AddForce(Vector2.down * shotKnockbackValue, ForceMode2D.Impulse);
    }
}
