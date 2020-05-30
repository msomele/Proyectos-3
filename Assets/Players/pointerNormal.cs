using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointerNormal : MonoBehaviour
{
    void Update()
    {
      
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            Quaternion aux = Quaternion.FromToRotation(Vector3.down, hit.normal);
            transform.rotation = new Quaternion(aux.x, 0, aux.y, aux.w);
        }
    }
}
