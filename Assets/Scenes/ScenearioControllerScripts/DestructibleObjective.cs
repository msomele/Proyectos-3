using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjective : MonoBehaviour
{
    public float objective_Hp;
    public bool isDestroyed;
    public bool hasPriority;
    public bool updateAIObjectives;
    private bool doOnce;
    public GameObject[] HitPositions;
    private void Start()
    {
        for (int i = 0; i < HitPositions.Length; i++)
        {
            HitPositions[i].GetComponent<HitPosition>().full = false;
        }
        doOnce = true;
        updateAIObjectives = false;
        isDestroyed = false;
        if (objective_Hp == 0f)
        {
            Debug.LogWarning("This Objective" + gameObject.name + "is destroyed immediately, as his HP is set to 0");
        }
    }

    private void Update()
    {
        if (objective_Hp <= 0 && doOnce)
        {
            doOnce = false;
            isDestroyed = true;
            DestroyObjective();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        objective_Hp -= damageAmount;
    }

    private void DestroyObjective()
    {
        updateAIObjectives = true;
        //Do destroy function here;
        gameObject.GetComponent<MeshCollider>().enabled = false;
    }
}
