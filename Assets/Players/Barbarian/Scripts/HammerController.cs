using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{

    public Material mat;
    public float intensity = 3;
    Color baseColor = Color.white;
   
    void Update()
    {
        
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(intensity);
        mat.SetColor("_EmissionColor", finalColor);

    }

   //cuando se haga la animación de hit, cambiar como se ha hecho en el default pulse a una animación el parametro intensity
}
