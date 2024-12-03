using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail_detecter : MonoBehaviour
{
    Snail snail;

    // Start is called before the first frame update
    void Start()
    {
        snail = FindNearestWithTag("snail").GetComponent<Snail>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            snail.SetFace();

            this.transform.localPosition = new Vector3(this.transform.localPosition.x, -this.transform.localPosition.y, 0);
        }
    }


    GameObject FindNearestWithTag(string tag)
    {
        // Find all objects with the specified tag
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        GameObject nearestObject = null;
        float shortestDistance = Mathf.Infinity;

        // Get the current object's position
        Vector3 currentPosition = transform.position;

        // Loop through all tagged objects to find the closest one
        foreach (GameObject obj in taggedObjects)
        {
            // Calculate the distance between the current object and the tagged object
            float distanceToObj = Vector3.Distance(currentPosition, obj.transform.position);

            // If this distance is shorter than the current shortest distance, update it
            if (distanceToObj < shortestDistance)
            {
                shortestDistance = distanceToObj;
                nearestObject = obj;
            }
        }

        return nearestObject;
    }
}
