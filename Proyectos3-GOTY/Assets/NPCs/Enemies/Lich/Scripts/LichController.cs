using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LichController : EnemyAgent
{
    public Transform[] summoningPositions;
    public int maxSummons;
    public int currentSummons;
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

    /*----------Objet Pooling-----------------*/
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                
                if (obj.GetComponent<SkeletonController>())
                {
                    obj.GetComponent<SkeletonController>().agentState = AgentStates.Dead;
                }
                
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);

        }
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
            if (IsObjectiveOnSpawningRange(spawningRange))
            {
                if (currentSummons < maxSummons)
                {
                    SummongSkeletons();
                }
                
            }
        }
    }
    private void SummongSkeletons()
    {
        Debug.Log("Summoning");
        int summongAmount = 0;
        currentSummons = 0;
        foreach (GameObject skeleton in poolDictionary["Skeleton"])
        {
            if (skeleton.GetComponent<SkeletonController>().agentState == AgentStates.Dead)
            {
                summongAmount++;
            }
        }

        //int summongAmountt  = maxSummons - currentSummons;
        for (int i = 0; i < summongAmount; i++)
        {
            SpawnFromPool("Skeleton", summoningPositions[i].position, summoningPositions[i].rotation);
        }
        summoning = true;
        RotateTowards(current_destination);
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        StartCoroutine(ResumeChasing(4f));
        summongAmount = 0;
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

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + "does not exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
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
