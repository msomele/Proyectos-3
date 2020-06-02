using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemController : EnemyAgent
{
    public GameObject current_objective;

    [Header("Speed")]
    public float minimum_Speed;
    public float maximun_Speed;

    [HideInInspector]
    public float nextAttack;

    private Animator animator;
    [HideInInspector]
    public bool risen;
    
    private Rigidbody rb;
    public float meleeRange;
    SkinnedMeshRenderer[] childrenRenderer;
    private float currentDisolveValue;
    // Start is called before the first frame update

    void OnEnable()
    {
        currentDisolveValue = 1;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        nextAttack = 0f;
        agentState = AgentStates.Idle;
        range = meleeRange;
        risen = false;
        agent = GetComponent<NavMeshAgent>();
        current_objective = GameObject.FindWithTag("CurrentEnemyObjective");
        current_destination = current_objective.transform.position;

        GetComponent<NavMeshAgent>().isStopped = false;

        childrenRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var r in childrenRenderer)
        {
            r.material.SetFloat("Alpha", 1);
        }
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (agentState != AgentStates.Dead)
        {

            animator.SetFloat("Speed", agent.desiredVelocity.magnitude);
            current_destination = current_objective.transform.position;
            if (risen && agentState != AgentStates.Ragdolled)
            {
                SetDestinationPoint(current_destination);
                if (IsObjectiveOnAttackRange(range))
                {
                    StopChasing();
                    nextAttack += Time.time;
                    if (nextAttack >= attackRate)
                    {
                        animator.SetBool("Attack", true);
                        nextAttack = 0f;
                        Attack();
                    }
                }
                else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    ChaseTarget();
                }
            }
        }

        foreach (var r in childrenRenderer)
        {
            r.material.SetFloat("Vector1_2E82CA3D", currentDisolveValue);
        }
    }

    private bool RandomizeBool()
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

    private bool IsObjectiveOnAttackRange(float range)
    {
        float currentDistance = Vector3.Distance(transform.position, current_destination);
        if (currentDistance <= range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Attack()
    {
        agentState = AgentStates.Attack;
    }

    public void DamageObjective()
    {
        if (IsObjectiveOnAttackRange(range))
        {
            if (current_objective.GetComponent<HitPosition>())
            {
                DestructibleObjective objective = current_objective.GetComponentInParent(typeof(DestructibleObjective)) as DestructibleObjective;
                objective.TakeDamage(attack_damage);
                if (objective.GetComponentInParent(typeof(Animator)))
                {
                    Animator objectiveAnimator = objective.GetComponentInParent(typeof(Animator)) as Animator;
                    objectiveAnimator.Play("Hit");
                }
            }
        }
    }

    public void ChaseTarget()
    {
        if (agentState != AgentStates.Ragdolled)
        {
            agentState = AgentStates.Running;
            nextAttack = 0f;
            SetDestinationPoint(current_destination);
            agent.isStopped = false;
            agent.velocity = agent.desiredVelocity;
        }
    }

    public void StopChasing()
    {
        RotateTowards(current_destination);
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timeSpawning);
        SetDestinationPoint(current_destination);
        InitializeRandomSpeed(minimum_Speed, maximun_Speed);
        risen = true;
    }

    public void StopAttackbool()
    {
        animator.SetBool("Attack", false);
    }

    public void TakeDamage(float damageAmount)
    {
        if (hp > 0f)
        {
            animator.CrossFade("Take Damage", 0.2f);
            hp -= damageAmount;
            if (hp <= 0f)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        agentState = AgentStates.Dead;
        animator.CrossFade("Die", 0.2f);
        StartCoroutine(Disolve(2f));
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        agent.enabled = false;
        Destroy(gameObject, 4f);
    }

    public IEnumerator FadeOut(float duration)
    {
        Material mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        while (mat.color.a > 0)
        {
            Color newColor = mat.color;
            newColor.a -= Time.deltaTime / duration;
            mat.color = newColor;
            yield return null;
        }
    }

    IEnumerator Disolve(float waitTime)
    {
        float duration = 2f;
        int target = 0;
        float start = 1f;
        yield return new WaitForSeconds(waitTime);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            currentDisolveValue = Mathf.Lerp(start, target, progress);
            yield return null;
        }
    }
}
