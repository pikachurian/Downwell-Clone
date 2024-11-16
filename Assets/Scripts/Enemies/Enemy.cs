using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int shotHPMax = 3;
    public int stompedHPMax = 1;

    private int shotHP = 3;
    private int stompedHP = 1;

    private void Start()
    {
        shotHP = shotHPMax;
        stompedHP = stompedHPMax;
    }

    public void TakeStompDamage(int amount )
    {
        stompedHP -= amount;

        if (stompedHP <= 0)
            Die();
    }

    public void TakeShotDamage(int amount)
    {
        shotHP -= amount;

        if (shotHP <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
