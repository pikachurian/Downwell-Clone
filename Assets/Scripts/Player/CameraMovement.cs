using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float startX;
    public float cameraLerpValue = 0.1f;
    public playerController player;
    public float yOffset = 0f;
    public float shakeStrength = 5f;
    private float startZ;

    public float shakeTime = 1f;
    private float shakeTick = 0f;

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

        //Shake
        if (shakeTick > 0f)
        {
            ShakeUpdate();
            shakeTick -= Time.deltaTime;
        }
    }

    public void CameraShake(float seconds, float strength)
    {
        shakeTick = seconds;
        shakeStrength = strength;
    }

    private void ShakeUpdate()
    {
        Vector3 cameraOffset = new Vector3(0f, 0f, startZ);

        cameraOffset.x = Random.Range(-1f, 1f) * shakeStrength;
        cameraOffset.y = Random.Range(-1f, 1f) * shakeStrength;

        transform.position += cameraOffset;
    }
}
