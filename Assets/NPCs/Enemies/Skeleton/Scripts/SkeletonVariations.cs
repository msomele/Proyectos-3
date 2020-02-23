using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonVariations : MonoBehaviour
{
    public GameObject[] Swords;
    public GameObject[] Helmets;
    // Start is called before the first frame update
    void Start()
    {
        RandomiceEquipment();
    }

    private void RandomiceEquipment()
    {
        int RandomSwordID = UnityEngine.Random.Range(0,Swords.Length);
        int RandomHelmetID = UnityEngine.Random.Range(0, Helmets.Length + 3);
        //Debug.Log("Sword: " + RandomSwordID);
        //Debug.Log("Helmet: " + RandomHelmetID);
        //Randomize Sword
        for (int i = 0; i < Swords.Length; i++)
        {
            if (i != RandomSwordID)
            {
                Swords[i].SetActive(false);
            }
        }
        //Randomize Helmet
        if (RandomHelmetID > Helmets.Length)
        {
            for (int i = 0; i < Helmets.Length; i++)
            {
                Helmets[i].SetActive(false);
            }
        }else
        {
            for (int i = 0; i < Helmets.Length; i++)
            {
                if (i != RandomHelmetID)
                {
                    Helmets[i].SetActive(false);
                }
            }
        }
    }
}
