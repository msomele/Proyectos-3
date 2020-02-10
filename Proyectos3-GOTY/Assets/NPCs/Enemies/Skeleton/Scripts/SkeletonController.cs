using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : EnemyAgent
{
    public NavMeshAgent agent;
    [Header("Speed")]
    public float minimum_Speed;
    public float maximun_Speed;
    [HideInInspector]
    public bool roar;
    private bool risen;
    private float timeSpawning;

    // Start is called before the first frame update
    void Start()
    {
        risen = false;
        agent = GetComponent<NavMeshAgent>();
        current_destination = GameObject.FindWithTag("CurrentEnemyObjective").transform.position;
        roar = randomizeBool();
        timeSpawning = roar? 6.5f : 4.5f;
        StartCoroutine(RiseFormTheDead());
    }
    private bool randomizeBool()
    {
        int aux = Random.Range(0, 2);
        if (aux == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (risen)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                agent.Move(agent.desiredVelocity);
            }
            else
            {
                agent.Move(Vector3.zero);
            }
        }
    }

    IEnumerator RiseFormTheDead()
    {
        yield return new WaitForSeconds(timeSpawning);
        SetDestinationPoint(current_destination);
        InitializeRandomSpeed(minimum_Speed, maximun_Speed);
        risen = true;
    }

    public void SetDestinationPoint(Vector3 Destination)
    {
        agent.SetDestination(Destination);
    }

    public void InitializeRandomSpeed(float min, float max)
    {
        agent.speed = Random.Range(min, max);
    }
}
