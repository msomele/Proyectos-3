using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        gameObject.GetComponent<Animator>().Play("Star");
    }
}
