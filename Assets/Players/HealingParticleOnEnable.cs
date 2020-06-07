using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingParticleOnEnable : MonoBehaviour
{

    private ParticleSystem[] particulas; 

    private void OnEnable()
    {
        particulas = this.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem particula in particulas)
        {
            particula.Play();
        }
    }
}
