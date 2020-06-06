using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLastVictory : MonoBehaviour
{
    public GameObject ScenenarioLogic;
    public GameObject[] thispull;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EveryoneIsdead())
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
