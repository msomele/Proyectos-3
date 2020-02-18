using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAgent : MonoBehaviour
{

    [HideInInspector]
    public NavMeshAgent agent;

    public enum AgentStates
    {
        Stunned,
        Running,
        Dead,
        Attack,
        Idle,
        Ragdolled
    }
    //[HideInInspector]
    public AgentStates agentState;

    [Header("Stats")]
    public float hp;
    public float attack_damage;
    [HideInInspector]
    public float speed;
    public float range;
    public float attackRate;
    public float timeSpawning;
    [HideInInspector]
    public Vector3 current_destination;

    //TODO
    //maybe add elementary states?

    public void SetDestinationPoint(Vector3 Destination)
    {
        agent.SetDestination(Destination);
    }

    public void InitializeRandomSpeed(float min, float max)
    {
        agent.speed = Random.Range(min, max);
    }
}
