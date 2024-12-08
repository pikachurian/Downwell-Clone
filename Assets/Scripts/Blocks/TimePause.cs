using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePause : MonoBehaviour
{
    //public GameObject player;
    public bool timeStop;

    // Start is called before the first frame update
    void Start()
    {

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
            timeStop = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            timeStop = false;
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
