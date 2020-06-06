using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPullOfEnemies : MonoBehaviour
{
    public GameObject ForcefieldGenerator;
    public GameObject[] EnemiesToSpawn;
    private bool once;
    // Start is called before the first frame update

    private void Start()
    {
        DisableEnemies();
        once = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (ForcefieldGenerator.GetComponent<DestructibleObjective>().isDestroyed == true && !once)
        {
            once = true;
            SpawnPull();
        }
    }

    private void DisableEnemies()
    {
        for (int i = 0; i < EnemiesToSpawn.Length; i++)
        {
            if (EnemiesToSpawn[i] != null)
            {
                EnemiesToSpawn[i].SetActive(false);
            }
            else
            {
                Debug.Log("SpawnPull: " + gameObject.name + " has index value nº" + i + " empty");
            }
        }
    }

    void SpawnPull()
    {
        for (int i = 0; i < EnemiesToSpawn.Length; i++)
        {
            if (EnemiesToSpawn[i] != null)
            {
                EnemiesToSpawn[i].SetActive(true);
            }
            else
            {
                Debug.Log("SpawnPull: " + gameObject.name + " has index value nº" + i + " empty");
            }

        }
    }
}
