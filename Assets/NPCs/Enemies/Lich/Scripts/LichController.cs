using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LichController : EnemyAgent
{
    public ParticleSystem ParticleBody;
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
    public GameObject bulletPrefab;
    bool fistSpawn = true;
    Animator lichAnimator;
    SkinnedMeshRenderer[] childrenRenderer;
    private float currentDisolveValue;
    public GameObject bulletSpawnPostion;
    private float nextDamageEvent;

    void Start()
    {
        nextDamageEvent = 0f;
        currentDisolveValue = 1f;
        childrenRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        lichAnimator = GetComponent<Animator>();
        lichAnimator.SetBool("Die", false);
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
        StartCoroutine(Materialize(1f));
        StartCoroutine(Spawn());
        agent.stoppingDistance = attackRange - 0.5f;
        ParticleBody.Stop();
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
    }

    private void Update()
    {
        RotateTowards(current_destination);
        nextDamageEvent += Time.deltaTime;
        //ANIMATIONS
        lichAnimator.SetFloat("Speed", agent.desiredVelocity.magnitude);
        lichAnimator.SetBool("Summoning", summoning);
        lastSummoned += Time.deltaTime;
        current_destination = current_objective.transform.position;
        if (spawned)
        {
            SetDestinationPoint(current_destination);
            if (IsObjectiveOnAttackRange(range) && !summoning && nextDamageEvent > attackRate)
            {
                nextDamageEvent = 0f;
                Attack();
            }

            if (IsObjectiveOnSpawningRange(spawningRange) && lastSummoned >= summongCooldown && !agent.isOnOffMeshLink)
            {  
                SummongSkeletons();
            }
        }
        foreach (var r in childrenRenderer)
        {
            r.material.SetFloat("Vector1_FEFF47F1", currentDisolveValue);
        }
    }

    private void Attack()
    {
        if (hp >0 )
        {
            RotateTowards(current_destination);
            lichAnimator.CrossFade("Attack", 0.3f);
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            StartCoroutine(ResumeChasing(2f));
        }
    }

    public void FireFireball()
    {
        Instantiate(bulletPrefab, bulletSpawnPostion.transform.position, bulletSpawnPostion.transform.rotation);
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
        //Debug.Log(numSkeletonsToSpawn);
        return numSkeletonsToSpawn;
    }
    
    private void SummongSkeletons()
    {
        bool ActualySummoned = false;
        if (fistSpawn)
        {
            //Debug.Log("Check");
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

    public void TakeDamage(float damageAmount)
    {
        if (hp > 0f)
        {
            lichAnimator.CrossFade("Take Damage", 0.2f);
            hp -= damageAmount;
            if (hp <= 0f)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        ParticleBody.Stop();
        lichAnimator.CrossFade("Die", 0.2f);
        lichAnimator.SetBool("Die", true);
        StartCoroutine(Disolve(2f)); 
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    IEnumerator ResumeChasing(float delay)
    {
        yield return new WaitForSeconds(delay);
        RotateTowards(current_destination);
        agent.isStopped = false;
        agent.velocity = agent.desiredVelocity;
        summoning = false;
    }

    IEnumerator Disolve(float waitTime)
    {
        float duration = 3f;
        int target = 1;
        float start = 0f;
        yield return new WaitForSeconds(waitTime);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            currentDisolveValue = Mathf.Lerp(start, target, progress);
            yield return null;
        }
        agentState = EnemyAgent.AgentStates.Dead;

        Destroy(gameObject);
    }

    IEnumerator Materialize(float waitTime)
    {
        float duration = 1f;
        int target = 0;
        float start = 1f;
        yield return new WaitForSeconds(waitTime);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            currentDisolveValue = Mathf.Lerp(start, target, progress);
            yield return null;
        }
        ParticleBody.Play();
    }
}
