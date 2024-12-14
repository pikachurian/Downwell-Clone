using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePause : MonoBehaviour
{
    //public GameObject player;
    public bool timeStop;

    public TimeStopManager timeStopManager;



    // Start is called before the first frame update
    void Start()
    {
        timeStopManager = GameObject.FindGameObjectWithTag("TimeStopManager").GetComponent<TimeStopManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Previous
        //if (Input.GetKeyDown("i"))
        //{
        //    FreezeTime();
        //}
        //if (Input.GetKeyDown("o"))
        //{
        //    UnfreezeTime();
        //}
        Debug.Log(timeStop);
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            /*timeStop = true;

            //Freeze gems.
            GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
            for (int i = 0; i < gems.Length; i ++)
            {
                Rigidbody2D rb = gems[i].GetComponent<Rigidbody2D>();
                rb.simulated = false;
            }*/
            timeStopManager.PauseTime();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            /*timeStop = false;

            //Freeze gems.
            GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
            for (int i = 0; i < gems.Length; i++)
            {
                Rigidbody2D rb = gems[i].GetComponent<Rigidbody2D>();
                rb.simulated = true;
            }*/

            timeStopManager.ResumeTime();
        }
    }

    //Previous
    //void FreezeTime()
    //{
    //    Time.timeScale = 0f; // Freezes the game
    //}
    //
    //void UnfreezeTime()
    //{
    //    Time.timeScale = 1f; // Resumes the game
    //}

    //void FreezeTime()
    //{
    //    MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
    //    foreach (MonoBehaviour script in allScripts)
    //    {
    //        if (script.gameObject.name != "Player" && script.gameObject.name != "Main Camera")
    //        {
    //            script.enabled = false;
    //        }
    //    }
    //}
    //
    //void UnfreezeTime()
    //{
    //    MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
    //    foreach (MonoBehaviour script in allScripts)
    //    {
    //        script.enabled = true;
    //    }
    //}
}
