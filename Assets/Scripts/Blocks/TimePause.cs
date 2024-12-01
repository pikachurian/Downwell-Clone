using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePause : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            FreezeTime();
        }
        if (Input.GetKeyDown("o"))
        {
            UnfreezeTime();
        }
    }
    void FreezeTime()
    {
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            if (script.gameObject != player)
            {
                script.enabled = false;
            }
        }
    }

    void UnfreezeTime()
    {
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour script in allScripts)
        {
            script.enabled = true;
        }
    }
}
