using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LichController : EnemyAgent
{
    public Transform[] summoningPositions;
    public int maxSummons;
    public GameObject SummogPrefab;
    public GameObject current_objective;
    public float attackRange;
    public float spawningRange;
    [Header("Speed")]
    public float minimum_Speed;
    public float maximun_Speed;
    private bool spawned;
    private Rigidbody rb;
    private bool summoning;
    public float summongCooldown;
    public float lastSummoned;
    public int summongAmount;
    public GameObject[] skeleton;
    bool fistSpawn = true;
    Animator lichAnimator;

    void Start()
    {
        lichAnimator = GetComponent<Animator>();
        for (int i = 0; i < 4; i++)
        {
           skeleton[i] = SummogPrefab;
        }
        lastSummoned = 0f;
        summoning = false;
        spawned = false;
        timeSpawning = 2f;
        rb = GetComponent<Rigidbody>();
        agentState = AgentStates.Idle;
        range = attackRange;
        agent = GetComponent<NavMeshAgent>();
        current_objective = GameObject.FindWithTag("CurrentEnemyObjective");
        current_destination = current_objective.transform.position;
        StartCoroutine(Spawn());
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
    }


    private void Update()
    {
        //ANIMATIONS
        lichAnimator.SetFloat("Speed", agent.desiredVelocity.magnitude);
        lichAnimator.SetBool("Summoning", summoning);
        lastSummoned += Time.deltaTime;
        current_destination = current_objective.transform.position;
        if (spawned)
        {
            SetDestinationPoint(current_destination);
            if (IsObjectiveOnAttackRange(range) && !summoning)
            {
                /*
                Attack
                */
            }
            if (IsObjectiveOnSpawningRange(spawningRange) && lastSummoned >= summongCooldown)
            {
                
                SummongSkeletons();
            }
        }
    }

    private int CheckNumSkeletonsToSpawn()
    {
        int numSkeletonsToSpawn = 0;
        for (int i = 0; i < maxSummons; i++)
        {
            if (skeleton[i] == null)
            {
                //numSkeletonsToSpawn++;
            }
            else { 
                if (skeleton[i].GetComponent<SkeletonController>().agentState == AgentStates.Ragdolled || skeleton[i].GetComponent<SkeletonController>().agentState == AgentStates.UnSpawned)
                {
                    numSkeletonsToSpawn++;
                }
            }

        }
        Debug.Log(numSkeletonsToSpawn);
        return numSkeletonsToSpawn;
    }
    
    private void SummongSkeletons()
    {
        bool ActualySummoned = false;
        if (fistSpawn)
        {
            Debug.Log("Check");
            summongAmount = 4;
            if (summongAmount <= maxSummons && summongAmount > 0)
            {
                for (int i = 0; i < summongAmount; i++)
                {
                    //skeleton[i] = SummogPrefab;
                    skeleton[i] = Instantiate(SummogPrefab, summoningPositions[i].position, summoningPositions[i].rotation);
                    lastSummoned = 0f;
                }
                summoning = true;
                RotateTowards(current_destination);
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                StartCoroutine(ResumeChasing(4f));
                summongAmount = 0;
            }
            fistSpawn = false;
        }
        else
        {
            Debug.Log("Check");
            summongAmount = CheckNumSkeletonsToSpawn();
            for (int i = 0; i < maxSummons; i++)
            {
                if (skeleton[i] == null)
                {
                    skeleton[i] = Instantiate(SummogPrefab, summoningPositions[i].position, summoningPositions[i].rotation);
                    ActualySummoned = true;
                    summongAmount -= 1;
                }
            }
            if (summongAmount <= maxSummons && summongAmount > 0)
            {

                for (int i = 0; i < summongAmount; i++)
                {
                    skeleton[i] = Instantiate(SummogPrefab, summoningPositions[i].position, summoningPositions[i].rotation);
                    ActualySummoned = true;
                    lastSummoned = 0f;
                }
            }
            if (ActualySummoned)
            {
                summoning = true;
                RotateTowards(current_destination);
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                StartCoroutine(ResumeChasing(4f));
                summongAmount = 0;
            }

        }
    }

    private bool IsObjectiveOnAttackRange(float range)
    {
        float CurrentDistance = Vector3.Distance(transform.position, current_destination);
        if (CurrentDistance <= range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsObjectiveOnSpawningRange(float _spawningRange)
    {
        float currentDistance = Vector3.Distance(transform.position, current_destination);
        if (currentDistance <= _spawningRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    IEnumerator Spawn()
    {   
        yield return new WaitForSeconds(timeSpawning);
        SetDestinationPoint(current_destination);
        InitializeRandomSpeed(minimum_Speed, maximun_Speed);
        spawned = true;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, spawningRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    IEnumerator ResumeChasing(float delay)
    {
        yield return new WaitForSeconds(delay);
        RotateTowards(current_destination);
        agent.isStopped = false;
        agent.velocity = agent.desiredVelocity;
        summoning = false;
    }
}
