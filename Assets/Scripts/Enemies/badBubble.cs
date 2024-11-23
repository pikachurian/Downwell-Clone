using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class badBubble : MonoBehaviour
{
    public bool isAwake = false;
    public GameObject player;
    public float maxSpeed = 2f;
    public float initalSpeed = 0.7f;
    public float acceleration = 0.5f; 
    public float speed;
    public float awakeDistance;
    public Rigidbody2D rb;
    public Vector2 direction;
    public bool isChasingPlayer;
    public bool isBouncing;
    public float bouceXdistance = 0.5F;
    public float bouceYdistance = 1F;
    public bool left_or_right;
    public Vector3 target;
    public bool firstBoucefinished = false;
    public bool secondBoucestarted = false;
    private enum State { 
        awake,
        chase,
        bounce,
        death
        }
    private State state = State.awake;
    public Enemy enemy;
    public float shotKnockbackValue = 2f;


    // Start is called before the first frame update
    void Start()
    {


        DOTween.SetTweensCapacity(500, 50);
        speed = initalSpeed;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        //rigidbody = this.rigidbody;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.awake:
                Awaking();
                break;
            case State.chase:
                Chasing();
                break;
            case State.bounce:
                Bounce();
                break;
            case State.death:
                break;
        }
    }

    public void Awaking()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= awakeDistance)
        {
            speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
            state = State.chase;
        }
    }

    public void Chasing()
    {
        direction = (player.transform.position - transform.position).normalized;
        speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        
        
    }
    public void Bounce()
    {
        if (Vector3.Distance(this.transform.position, target)> 0.05f && !firstBoucefinished)
        {
            
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);

        }
        else if (Vector3.Distance(this.transform.position, target) < 0.05f)
        {
            firstBoucefinished = true;
            if (!secondBoucestarted)
            {
                if (left_or_right == false)
                {
                    target = new Vector3(this.transform.position.x + bouceXdistance, this.transform.position.y + bouceYdistance * 2, 0);
                    secondBoucestarted = true;
                }
                if (left_or_right == true)
                {
                    target = new Vector3(this.transform.position.x - bouceXdistance, this.transform.position.y + bouceYdistance * 2, 0);
                    secondBoucestarted = true;
                }
                
            }
        }
        if (firstBoucefinished && secondBoucestarted)
        {
            
            this.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            
            if (Vector3.Distance(this.transform.position, target) < 0.05f)
            {
                firstBoucefinished = false;
                secondBoucestarted = false;
                state = State.chase;
            }
        }





    }


    IEnumerator Bouce_move()
    {
        if (left_or_right == false)
        {
            this.transform.DOMove(new Vector3(this.transform.position.x - bouceXdistance, this.transform.position.y - bouceYdistance, 0), 0.75f).SetEase(Ease.InOutQuart).WaitForCompletion();

            // Move to the second position
            yield return this.transform
                .DOMove(new Vector3(this.transform.position.x - bouceXdistance, this.transform.position.y + bouceYdistance, 0), 0.75f)
                .SetEase(Ease.InOutQuart)
                .WaitForCompletion();
        }
        if (left_or_right == true)
        {
            yield return this.transform
            .DOMove(new Vector3(this.transform.position.x + bouceXdistance, this.transform.position.y - bouceYdistance, 0), 0.75f)
            .SetEase(Ease.InOutQuart)
            .WaitForCompletion();

            // Move to the second position
            yield return this.transform
                .DOMove(new Vector3(this.transform.position.x + bouceXdistance, this.transform.position.y + bouceYdistance, 0), 0.75f)
                .SetEase(Ease.InOutQuart)
                .WaitForCompletion();
        }
    }

    IEnumerator Bouce_wait()
    {
        yield return new WaitForSeconds(0.75f);

        if (left_or_right == false)
        {

            target = Vector3.MoveTowards(transform.position, new Vector3(this.transform.position.x + bouceXdistance, this.transform.position.y + bouceYdistance, 0), speed * Time.deltaTime);
            firstBoucefinished = true;
        }
        if (left_or_right == true)
        {

            target = Vector3.MoveTowards(transform.position, new Vector3(this.transform.position.x - bouceXdistance, this.transform.position.y + bouceYdistance, 0), speed * Time.deltaTime);
            firstBoucefinished = true;
        }
    }


        public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            state = State.bounce;
            if (this.transform.position.x < player.transform.position.x) { left_or_right = false; target = new Vector3(this.transform.position.x + bouceXdistance, this.transform.position.y - bouceYdistance, 0); }
            else if (this.transform.position.x > player.transform.position.x) { left_or_right = true;target = new Vector3(this.transform.position.x - bouceXdistance, this.transform.position.y - bouceYdistance, 0); }
            print(state);
            print(target);

           

        }


        if (collision.gameObject.CompareTag("bullet"))
        {
            //Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y > this.transform.position.y)
            {
                //Destroy(gameObject);
            }
        }

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
