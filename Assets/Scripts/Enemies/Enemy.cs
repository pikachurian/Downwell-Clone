using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int shotHPMax = 3;
    public int stompedHPMax = 1;

    public int gemsToSpawn = 2;
    public float gemSpawnSpeed = 10;
    public Vector3 gemSpawnOffset = Vector3.zero;
    public GameObject gemPrefab;

    public AudioSource audioSource;
    public AudioClip splatSound;

    private int shotHP = 3;
    private int stompedHP = 1;

    public float despawnTime = 1f;
    private float despawnTick = 0f;
    private bool isDead = false;

    private playerController player;

    private void Start()
    {
        shotHP = shotHPMax;
        stompedHP = stompedHPMax;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
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

        //Spawn Gems.
        SpwanGem();

        //Add to combo.
        player.AddToCombo(1);
    }

    void SpwanGem()
    {
        for (int i = 0; i < gemsToSpawn; i++)
        {
            GameObject gemInst = Instantiate(gemPrefab);
            float gemDir = 1f;
            if (Random.value < 0.5f)
                gemDir = -1f;
            gemInst.GetComponent<Rigidbody2D>().velocity = new Vector3(gemDir * 2f, 1f, 0f) * gemSpawnSpeed;
            gemInst.transform.position = transform.position + gemSpawnOffset;
        }
    }
}
