using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 teleportDestination;

    void Start()
    {
        // Manually set the teleport destination in code
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the trigger
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the specified destination
            other.transform.position = teleportDestination;

        }
    }
}
