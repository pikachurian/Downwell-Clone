using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Stomping
        //print(collision.gameObject.layer);
        if (collision.gameObject.layer == 9)//If on enemy layer
        {
            //print("Player stomped on enemy.");

            if (collision.transform.position.y > transform.position.y)
            {
                Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
                Vector2 force = knockbackDirection * 20f;

                if (collision.transform.position.x > transform.position.x + 0.4f)
                {


                    print(force);
                    switch (collision.gameObject.tag)
                    {
                        case "Bad_Bubble":

                            rigidbody.AddForce(force, ForceMode2D.Impulse);
                            


                            break;
                    }
                }





            }
        }
    }

}
