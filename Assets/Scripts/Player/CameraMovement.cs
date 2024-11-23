using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float startX;
    public float cameraLerpValue = 0.1f;
    public playerController player;
    public float yOffset = 0f;
    private float startZ;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(startX, Mathf.Lerp(transform.position.y, player.transform.position.y + yOffset, cameraLerpValue), startZ);
        transform.position = newPosition;
    }
}
