using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPull : MonoBehaviour
{
    //[HideInInspector]
    public GameObject[] ObjectivesArray = new GameObject[4];

    public GameObject[] EnemiesToSpawn;
    public float timeSpawning;
    bool enemyPoolSpawned;
    private float currenttime;
    private int currentIndexObjective;
    private GameObject objectiveToFeed;
    //0,1,2 are gates
    //3 - 4 are main objectives (priority)
    void Start()
    {
        AsingObjectiveToPull();
        DisableEnemies();
        enemyPoolSpawned = false;
    }

    private void Update()
    {
        if (enemyPoolSpawned)
        {
            OnValueChangedCheckObjectives();
        }
        else {
            currenttime += Time.deltaTime;
        }

        if (currenttime > timeSpawning && !enemyPoolSpawned)
        {
            SpawnPull();
            //gameObject.SetActive(false);
        }
    }

    private void OnValueChangedCheckObjectives()
    {
        for (int i = 0; i < ObjectivesArray.Length; i++)
        {
            if (ObjectivesArray[i].GetComponent<DestructibleObjective>().updateAIObjectives == true)
            {
                //Debug.Log("Updating objectives");
                AsingObjectiveToPull();
                //Debug.Log("Returning to normal objectives");
                //ObjectivesArray[i].GetComponent<DestructibleObjective>().updateAIObjectives = false;
                StartCoroutine(AllObjectivesUpdated(i));
            }
        }
    }

    IEnumerator AllObjectivesUpdated(int arrayIndex)
    {
        yield return new WaitForSeconds(0.5f);
        ObjectivesArray[arrayIndex].GetComponent<DestructibleObjective>().updateAIObjectives = false;
    }



    public float ReturnDistanceToObjective(Transform transform1, Transform transform2)
    {
        float dist = Vector3.Distance(transform1.position, transform2.position);
        return dist;
    }

    GameObject CheckIndexOfObjectivesToFeed(int enemyIndex)
    {
        
        if (ObjectivesArray[0] == null)
        {
            Debug.Log("Aun no maricarmen");
            return null;
        }

        //If both gates are up
        if (ObjectivesArray[0].GetComponent<DestructibleObjective>().isDestroyed == false && ObjectivesArray[1].GetComponent<DestructibleObjective>().isDestroyed == false)
        {
            //and its closer to main gate
            if (ReturnDistanceToObjective(EnemiesToSpawn[enemyIndex].transform, ObjectivesArray[0].transform) <
                ReturnDistanceToObjective(EnemiesToSpawn[enemyIndex].transform, ObjectivesArray[1].transform))
            {
                //Set Objective to main gate
                return ObjectivesArray[0];
            }
            else //if its not closer to main gate
            {
                return ObjectivesArray[1]; //go to secondary gate
            }

        }
        //If both gates are Down
        else if (ObjectivesArray[0].GetComponent<DestructibleObjective>().isDestroyed == true && ObjectivesArray[1].GetComponent<DestructibleObjective>().isDestroyed == true)
        {
            //Closed to tertiary
            if (ReturnDistanceToObjective(EnemiesToSpawn[enemyIndex].transform, ObjectivesArray[2].transform) <
            ReturnDistanceToObjective(EnemiesToSpawn[enemyIndex].transform, ObjectivesArray[3].transform))
            {
                if (ObjectivesArray[2].GetComponent<DestructibleObjective>().isDestroyed == false)
                {
                    return ObjectivesArray[2]; //go to tertiary gate
                }
            }
            //if not go to obelisk or angel
            else
            {
                if (ObjectivesArray[3].GetComponent<DestructibleObjective>().isDestroyed == false)
                {
                    return ObjectivesArray[3]; //go to obelisk
                }
                else
                {
                    return ObjectivesArray[4]; //go to angel
                }
            }
        }
        //If obelisck is down;
        if (ObjectivesArray[3].GetComponent<DestructibleObjective>().isDestroyed == true)
        {
            return ObjectivesArray[4]; //go to statue(final)
        }
        //if main gate is up but secondary gate is down
        if (ObjectivesArray[0].GetComponent<DestructibleObjective>().isDestroyed == false && ObjectivesArray[1].GetComponent<DestructibleObjective>().isDestroyed == true)
        {
            //Debug.Log("Main up Second down");
            //If is away from main gate
            if (ReturnDistanceToObjective(EnemiesToSpawn[enemyIndex].transform, ObjectivesArray[0].transform) > 40f) //THIS DISTANCE IS ARBITRARY
            {
                //If terciary gate is up
                if (ObjectivesArray[2].GetComponent<DestructibleObjective>().isDestroyed == false)
                {
                    return ObjectivesArray[2]; //go to tertiary gate
                }
                else //Iff Terciary gate is down
                {
                    return ObjectivesArray[3]; //go obelisk
                }
            }
            else
            {
                return ObjectivesArray[0]; //go main gate
            }
        }
        //if main gate is down but secondary gate is up
        if (ObjectivesArray[0].GetComponent<DestructibleObjective>().isDestroyed == true && ObjectivesArray[1].GetComponent<DestructibleObjective>().isDestroyed == false)
        {
            //Debug.Log("Main down Second up");
            //If is away from secondary gate
            if (ReturnDistanceToObjective(EnemiesToSpawn[enemyIndex].transform, ObjectivesArray[1].transform) > 30f)    //THIS DISTANCE IS ARBITRARY
            {
                //Debug.Log("Away from secundary gate");
                return ObjectivesArray[3]; //go to obelisk
            }
            else //If is close to secondary gate
            {
                //Debug.Log("close from secundary gate");
                return ObjectivesArray[1]; //go to secondarygate
            }
        }


        //Debug.Log("A SITUATION IS NOT DEFINED, RETURNING Obelisk");
        return ObjectivesArray[3];
    }

    void AsingObjectiveToPull()
    {

        //Find Objective to feed
        for (int i = 0; i < EnemiesToSpawn.Length; i++)
        {
            //Skeleton
            if (EnemiesToSpawn[i].GetComponent<SkeletonController>())
            {
                objectiveToFeed = CheckIndexOfObjectivesToFeed(i);
                int postion = CheckHitPositionsFreePosition();
                //Debug.Log("I am" + EnemiesToSpawn[i].name);
                //Debug.Log("My current objective is" + CheckIndexOfObjectivesToFeed(i).name);
                //Debug.Log("Calling from" + gameObject.name);
                EnemiesToSpawn[i].GetComponent<SkeletonController>().current_objective = objectiveToFeed.GetComponent<DestructibleObjective>().HitPositions[postion];
                objectiveToFeed.GetComponent<DestructibleObjective>().HitPositions[postion].GetComponent<HitPosition>().full = true;
            }

            //Lich
            if (EnemiesToSpawn[i].GetComponent<LichController>())
            {
                objectiveToFeed = CheckIndexOfObjectivesToFeed(i);
                int postion = CheckHitPositionsFreePosition();
                EnemiesToSpawn[i].GetComponent<LichController>().current_objective = objectiveToFeed.GetComponent<DestructibleObjective>().HitPositions[postion];
                objectiveToFeed.GetComponent<DestructibleObjective>().HitPositions[postion].GetComponent<HitPosition>().full = true;
            }

            //Golem
            if (EnemiesToSpawn[i].GetComponent<GolemController>())
            {
                objectiveToFeed = CheckIndexOfObjectivesToFeed(i);
                int postion = CheckHitPositionsFreePosition();
                EnemiesToSpawn[i].GetComponent<GolemController>().current_objective = objectiveToFeed.GetComponent<DestructibleObjective>().HitPositions[postion];
                objectiveToFeed.GetComponent<DestructibleObjective>().HitPositions[postion].GetComponent<HitPosition>().full = true;
            }
            //Asing hit position of that objective we found early
        }

        //Clear hit positions for next iteration
        for (int l = 0; l < objectiveToFeed.GetComponent<DestructibleObjective>().HitPositions.Length - 1; l++)
        {
            objectiveToFeed.GetComponent<DestructibleObjective>().HitPositions[l].GetComponent<HitPosition>().full = false;
        }

    }

    private int CheckHitPositionsFreePosition()
    {
        for (int j = 0; j < objectiveToFeed.GetComponent<DestructibleObjective>().HitPositions.Length -1; j++)
        {
            if (objectiveToFeed.GetComponent<DestructibleObjective>().HitPositions[j].GetComponent<HitPosition>().full == false)
            {
                return j;
            }
        }
        return 4;
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
        enemyPoolSpawned = true;
        AsingObjectiveToPull();

    }
}
