using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnEnable : MonoBehaviour
{

    private void OnEnable()
    {
        this.GetComponentInChildren<ParticleSystem>().Play();
    }
}
