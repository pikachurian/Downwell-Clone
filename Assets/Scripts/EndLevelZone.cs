using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelZone : MonoBehaviour
{
    public LevelFade levelFade;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelFade.LevelEndReached();
        }
    }
}
