using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    private Enemy enemyScript;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.velocity = Vector2.zero;
    }

    public void TakeStompDamage(int amount)
    {
        enemyScript.TakeStompDamage(amount);
    }

    public void TakeShotDamage(int amount)
    {
        //Does not take shot damage.
    }
}
