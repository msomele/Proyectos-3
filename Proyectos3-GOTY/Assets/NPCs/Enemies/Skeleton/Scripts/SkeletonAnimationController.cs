using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAnimationController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    private SkeletonController skeletonController;
    private EnemyAgent enemyAget;
    private bool once = false;
    public bool attack;
    // Start is called before the first frame update
    void Start()
    {
        enemyAget = GetComponent<EnemyAgent>();
        skeletonController = GetComponent<SkeletonController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (navMeshAgent.isOnOffMeshLink)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        if (!once)
        {
            animator.SetBool("Roar", skeletonController.roar);
            once = true;
        }
        animator.SetFloat("Speed", navMeshAgent.desiredVelocity.magnitude);
        animator.SetBool("Attack", attack);
    }
}
