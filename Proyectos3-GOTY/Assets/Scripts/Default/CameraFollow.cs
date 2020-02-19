using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerFollow;
    public float zoom = 5f;
    private void FixedUpdate()
    {
        transform.position = new Vector3(playerFollow.position.x, transform.position.y, playerFollow.position.z - zoom);
    }
}
