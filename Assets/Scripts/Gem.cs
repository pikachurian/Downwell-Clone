using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1f;
    public float bounceSpeed = 0.5f;

    private void Start()
    {
        rb.velocity = new Vector3(2f, 1f, 0f) * speed;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.CompareTag("Ground"))
        {
            // Magnitude of the velocity vector is speed of the object (we will use it for constant speed so object never stop)
            //float speed = rb.velocity.magnitude;

            // Reflect params must be normalized so we get new direction
            Vector3 direction = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);

            // Like earlier wrote: velocity vector is magnitude (speed) and direction (a new one)
            rb.velocity = direction * bounceSpeed;
        }
    }
}
