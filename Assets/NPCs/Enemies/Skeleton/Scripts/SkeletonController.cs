using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : EnemyAgent
{
    public GameObject current_objective;
    
    [Header("Speed")]
    public float minimum_Speed;
    public float maximun_Speed;

    [HideInInspector]
    public bool roar;
    [HideInInspector]
    public float nextAttack;
    public float meleeRange;
    private SkeletonAnimationController animController;
    private Animator animator;
    [HideInInspector]
    public bool risen;

    private GameObject[] objectives;

    private Rigidbody rb;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if (current_objective == null)
        {
            objectives = GameObject.FindGameObjectsWithTag("CurrentEnemyObjective");
            current_destination = GetClosestObjective(objectives).transform.position;
        }
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.Play("Hidden");
        nextAttack = 0f;
        agentState = AgentStates.Idle;
        range = meleeRange;
        risen = false;
        agent = GetComponent<NavMeshAgent>();
        animController = gameObject.GetComponent<SkeletonAnimationController>();
        animController.attack = false;
        roar = RandomizeBool();
        timeSpawning = roar ? 6.5f : 5f;

        GetComponent<Animator>().enabled = true;
        GetComponent<SkeletonController>().enabled = true;
        GetComponent<SkeletonAnimationController>().enabled = true;
        GetComponent<AgentLinkMover>().enabled = true;
        GetComponent<NavMeshAgent>().isStopped = false;

        SkinnedMeshRenderer[] childrenRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var r in childrenRenderer)
        {
            r.material.SetFloat("Dissolve", 0);
        }
        GetComponent<SkeletonRagdoll>().setRigidBodyState(true);
        GetComponent<SkeletonRagdoll>().setCollidersState(false);
        GetComponent<SkeletonRagdoll>().currentDisolveValue = 0f;
        rb.isKinematic = true;
        StartCoroutine(RiseFormTheDead());
    }

    GameObject GetClosestObjective(GameObject[] objectives)
    {
        float CurrentMinDistance = 50000;
        GameObject objective = null;
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i].GetComponent<DestructibleObjective>().isDestroyed == false)
            {
                float distance = Vector3.Distance(transform.position, objectives[i].transform.position);
                if (distance < CurrentMinDistance)
                {
                    CurrentMinDistance = distance;
                    objective = objectives[i];
                }
            }

        }
        return objective;
    }

    void Start()
    {
        if (current_objective == null)
        {
            objectives = GameObject.FindGameObjectsWithTag("CurrentEnemyObjective");
            current_objective = GetClosestObjective(objectives);
            current_destination = current_objective.transform.position;
        }
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.Play("Hidden");
        nextAttack = 0f;
        agentState = AgentStates.Idle;
        range = meleeRange;
        risen = false;
        agent = GetComponent<NavMeshAgent>();
        animController = gameObject.GetComponent<SkeletonAnimationController>();
        animController.attack = false;
        //current_objective = GameObject.FindWithTag("CurrentEnemyObjective");
        //current_destination = current_objective.transform.position;
        roar = RandomizeBool();
        timeSpawning = roar? 6.5f : 5f;

        GetComponent<Animator>().enabled = true;
        GetComponent<SkeletonController>().enabled = true;
        GetComponent<SkeletonAnimationController>().enabled = true;
        GetComponent<AgentLinkMover>().enabled = true;
        GetComponent<NavMeshAgent>().isStopped = false;

        SkinnedMeshRenderer[] childrenRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var r in childrenRenderer)
        {
            r.material.SetFloat("Dissolve", 0);
        }
        GetComponent<SkeletonRagdoll>().setRigidBodyState(true);
        GetComponent<SkeletonRagdoll>().setCollidersState(false);
        GetComponent<SkeletonRagdoll>().currentDisolveValue = 0f;
        rb.isKinematic = true;
        StartCoroutine(RiseFormTheDead());
    }

    // Update is called once per frame
    void Update()
    {
        if (agentState != AgentStates.Dead) {
            current_destination = current_objective.transform.position;
            if (current_objective.GetComponent<DestructibleObjective>())
            {
                if (current_objective.GetComponent<DestructibleObjective>().isDestroyed == true)
                {
                    current_objective = GetClosestObjective(objectives);
                }
            }

            if (risen && agentState != AgentStates.Ragdolled)
            {
                SetDestinationPoint(current_destination);
                if (IsObjectiveOnAttackRange(range))
                {
                    StopChasing();
                    nextAttack += Time.time;
                    if (nextAttack >= attackRate)
                    {
                        nextAttack = 0f;
                        Attack();
                    }
                }else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    ChaseTarget();
                }
            }
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
        animController.attack = true;
    }

    public void DamageObjective()
    {
        if (IsObjectiveOnAttackRange(range))
        {
            //Debug.Log("Ataque Esqueleto");
            /*
             if (current_objective.GetComponent<PlayerHealth>())
             {
                 current_objective.GetComponent<PlayerHealth>().DeductHealth(attack_damage);
             }
            */
            if (current_objective.GetComponent<DestructibleObjective>())
            {
                current_objective.GetComponent<DestructibleObjective>().TakeDamage(attack_damage);
                if (current_objective.GetComponentInParent(typeof(Animator)))
                {
                    Animator objectiveAnimator = current_objective.GetComponentInParent(typeof(Animator)) as Animator;
                    objectiveAnimator.Play("Hit");
                }
            }
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
        else
        {
            animController.attack = false;
        }
    }

    public void StopAttackingAnimation()
    {
        animController.attack = false;
    }

    public void ChaseTarget()
    {
        if (agentState != AgentStates.Ragdolled)
        {
            animController.attack = false;
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
        animController.attack = false;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));   
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
    }

    IEnumerator RiseFormTheDead()
    {
        yield return new WaitForSeconds(timeSpawning);
        SetDestinationPoint(current_destination);
        InitializeRandomSpeed(minimum_Speed, maximun_Speed);
        risen = true;
    }
}
