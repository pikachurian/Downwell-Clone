using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gempile : MonoBehaviour
{
    private Enemy enemyScript;
    public GameObject gemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<Enemy>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeStompDamage(int amount)
    {
       // enemyScript.TakeStompDamage(amount);
    }

    public void TakeShotDamage(int amount)
    {
        enemyScript.TakeShotDamage(amount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
           // SpwanGem();
        }
    }
    public void SpwanGem()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject gemInst = Instantiate(gemPrefab);
            float gemDir = 1f;
            if (Random.value < 0.5f)
                gemDir = -1f;
            gemInst.GetComponent<Gem>().affectedByTimePause = false;
            gemInst.GetComponent<Rigidbody2D>().velocity = new Vector3(gemDir * 2f, 1f, 0f) * 8;
            gemInst.transform.position = transform.position + Vector3.zero;
            gemInst.GetComponent<Rigidbody2D>().simulated = true;
        }
    }
}
