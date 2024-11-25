using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int shotHPMax = 3;
    public int stompedHPMax = 1;

    public AudioSource audioSource;
    public AudioClip splatSound;

    private int shotHP = 3;
    private int stompedHP = 1;

    public float despawnTime = 1f;
    private float despawnTick = 0f;
    private bool isDead = false;

    private void Start()
    {
        shotHP = shotHPMax;
        stompedHP = stompedHPMax;
    }

    private void Update()
    {
        if (isDead)
        {
            if (despawnTick >= despawnTime)
                Destroy(this.gameObject);
            else
                despawnTick += Time.deltaTime;
        }
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
        audioSource.PlayOneShot(splatSound);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        isDead = true;
    }
}
