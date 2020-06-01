using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPosition : MonoBehaviour
{
    public bool full;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
