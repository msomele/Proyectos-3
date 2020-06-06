using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ÑapaPuerta : MonoBehaviour
{
    public GameObject firstgate;
    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<DestructibleObjective>().isDestroyed == true)
        {
            firstgate.GetComponent<DestructibleObjective>().objective_Hp = 0f;
        }
    }
}
