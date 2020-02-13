using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAgent : MonoBehaviour
{
    public enum AgentStates
    {
        Running,
        Dead,
        Attack,
        Idle
    }
    [HideInInspector]
    public AgentStates agentState;

    [Header("Stats")]
    public float hp;
    public float attack_damage;
    public float speed;
    public float range;
    public float attackRate;
    [HideInInspector]
    public Vector3 current_destination;

    //TODO
    //maybe add elementary states?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
