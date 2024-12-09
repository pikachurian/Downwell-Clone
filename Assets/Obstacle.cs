using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private Enemy enemyScript;
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
        enemyScript.TakeStompDamage(amount);
    }

    public void TakeShotDamage(int amount)
    {
        enemyScript.TakeShotDamage(amount);
    }
}
