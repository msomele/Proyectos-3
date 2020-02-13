using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : EnemyAgent
{
    public GameObject current_objective;
    public NavMeshAgent agent;
    [Header("Speed")]
    public float minimum_Speed;
    public float maximun_Speed;
    [HideInInspector]
    public bool roar;
    private Animator animator;
    private bool risen;
    private float timeSpawning;
    public float nextAttack;
    private SkeletonAnimationController animController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        nextAttack = 0f;
        agentState = AgentStates.Idle;
        range = 3f;
        risen = false;
        agent = GetComponent<NavMeshAgent>();
        animController = gameObject.GetComponent<SkeletonAnimationController>();
        current_objective = GameObject.FindWithTag("CurrentEnemyObjective");
        current_destination = current_objective.transform.position;
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
    // Update is called once per frame
    void Update()
    {
        current_destination = current_objective.transform.position;
        if (risen)
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

    void Attack()
    {
        agentState = AgentStates.Attack;
        animController.attack = true;
    }

    public void DamageObjective()
    {
        if (IsObjectiveOnAttackRange(range))
        {
            Debug.Log("Toma geromma");
            /*
             if (current_objective.GetComponent<PlayerHealth>())
             {
                 current_objective.GetComponent<PlayerHealth>().DeductHealth(attack_damage);
             }
            */
        }
        else
        {
            animController.attack = false;
        }
    }

    public void StopAttacking()
    {
        animController.attack = false;
    }

    public void ChaseTarget()
    {
        //animator.Play("Run");
        animController.attack = false;
        agentState = AgentStates.Running;
        nextAttack = 0f;
        SetDestinationPoint(current_destination);
        agentState = AgentStates.Running;
        agent.isStopped = false;
        
        agent.velocity = agent.desiredVelocity;
    }

    public void StopChasing()
    {
        RotateTowards(current_destination);
        animController.attack = false;
        //agentState = AgentStates.Idle;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
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
