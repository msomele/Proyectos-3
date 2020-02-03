using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerFollow;

    private void FixedUpdate()
    {
        transform.position = new Vector3(playerFollow.position.x, transform.position.y, playerFollow.position.z - 25);
    }
}
