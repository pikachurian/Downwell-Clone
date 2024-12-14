using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopManager : MonoBehaviour
{
    public bool isTimePaused = false;

    public void PauseTime()
    {
        isTimePaused = true;

        //Freeze gems.
        GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
        for (int i = 0; i < gems.Length; i++)
        {
            if (gems[i].GetComponent<Gem>().affectedByTimePause)
            {
                Rigidbody2D rb = gems[i].GetComponent<Rigidbody2D>();
                rb.simulated = false;
            }
        }
    }

    public void ResumeTime()
    {
        isTimePaused = false;

        //Freeze gems.
        GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
        for (int i = 0; i < gems.Length; i++)
        {
            if (gems[i].GetComponent<Gem>().affectedByTimePause)
            {
                Rigidbody2D rb = gems[i].GetComponent<Rigidbody2D>();
                rb.simulated = true;
            }
        }
    }

    public bool GetIsTimePaused()
    {
        return isTimePaused;
    }
}
