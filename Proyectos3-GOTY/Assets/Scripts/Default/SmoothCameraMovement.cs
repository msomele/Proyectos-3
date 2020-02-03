using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    public static Vector3 cameraOffset;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    
    void FixedUpdate()
    {
        cameraOffset = transform.position - playerTransform.position;
        Vector3 newPos = playerTransform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        
    }
}