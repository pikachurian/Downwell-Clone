using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badBubble : MonoBehaviour
{
    public bool isAwake = false;
    public GameObject player;
    public float speed;
    public float awakeDistance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Awaking();
        Moving_Enmey();
    }

    public void Awaking()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= awakeDistance)
        {
            isAwake = true;
        }
    }

    public void Moving_Enmey()
    {
        if (isAwake)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

}
