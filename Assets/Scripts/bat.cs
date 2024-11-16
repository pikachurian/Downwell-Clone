using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class bat : MonoBehaviour
{
    private enum State
    {
        idle,
        chase,
        death
    }
    private State currentState = State.idle;
    public float speed;
    public Vector2 direction;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
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

        Debug.Log(currentState);
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
    }
}
