using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskDestroy : MonoBehaviour
{
    public GameObject ForceField;
    public GameObject Fx;
    public GameObject BrokenObelisk;

    private DestructibleObjective dest;
    // Start is called before the first frame update
    void Start()
    {
        BrokenObelisk.SetActive(false);
        dest = gameObject.GetComponent<DestructibleObjective>();
        ForceField.SetActive(true);
        Fx.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (dest.isDestroyed)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            BrokenObelisk.SetActive(true);
            ForceField.SetActive(false);
            Fx.SetActive(false);
        }
    }
}
