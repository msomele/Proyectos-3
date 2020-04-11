using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScenarioController : MonoBehaviour
{
    public GameObject[] ObjectivesArray;
    public EnemyPull[] EnemyPulls;

    // Start is called before the first frame update
    void Awake()
    {
        TransferObjectivesToPulls();
    }

    void TransferObjectivesToPulls()
    {
        //Debug.Log(EnemyPulls.Length);
        for (int i = 0; i < EnemyPulls.Length; i++)
        {
            if (EnemyPulls[i] != null)
            {
                for (int j = 0; j < this.ObjectivesArray.Length; j++)
                {
                    EnemyPulls[i].ObjectivesArray[j] = this.ObjectivesArray[j];
                }
            }
        }
    }
}
