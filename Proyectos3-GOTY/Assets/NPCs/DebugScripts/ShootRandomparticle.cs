using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRandomparticle : MonoBehaviour
{
    public GameObject prefab;
    public Transform shootposition;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Instantiate(prefab, shootposition.position, transform.localRotation * Quaternion.Euler(new Vector3(0f, 180f, 0f)));
        }
    }
}
