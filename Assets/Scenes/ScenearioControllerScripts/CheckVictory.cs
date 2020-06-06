using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVictory : MonoBehaviour
{
    public GameObject ScenenarioLogic;
    public GameObject[] thispull;
    public GameObject forcefield;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EveryoneIsdead() && forcefield.activeSelf)
        {
            ScenenarioLogic.GetComponent<ScenarioController>().gameEnded = true;
        }
    }

    bool EveryoneIsdead()
    {
        for (int i = 0; i < thispull.Length; i++)
        {
            if (thispull[i] != null)
            {
                return false;
            }
        }
        return true;
    }
}
