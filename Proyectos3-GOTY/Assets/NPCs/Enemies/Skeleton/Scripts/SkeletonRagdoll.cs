using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonRagdoll : MonoBehaviour
{
    public float currentDisolveValue;
    public Material dissolveMaterial;
    SkinnedMeshRenderer[] childrenRenderer;
    private SkeletonController skeletonController;
    // Start is called before the first frame update

    void Start()
    {
        skeletonController = GetComponent<SkeletonController>();
        childrenRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        setRigidBodyState(true);
        setCollidersState(false);
    }

    private void Update()
    {
        foreach (var r in childrenRenderer)
        {
            r.material.SetFloat("Vector1_FEFF47F1", currentDisolveValue);
        }
    }
    // Update is called once per frame
    public void Die()
    {
        skeletonController.agentState = EnemyAgent.AgentStates.Ragdolled;
        StartCoroutine(Disolve(5f));
        setCollidersState(true);
        setRigidBodyState(false);
        GetComponent<Animator>().enabled = false;
        GetComponent<SkeletonAnimationController>().enabled = false;
        GetComponent<AgentLinkMover>().enabled = false;
        skeletonController.agent.velocity = Vector3.zero;
        skeletonController.agent.isStopped = true;

    }

    public void setRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    public void setCollidersState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        GetComponent<Collider>().enabled = !state;
    }

    IEnumerator Disolve(float waitTime)
    {
        
        float duration = 2f;
        int target = 1;
        float start = 0f;
        yield return new WaitForSeconds(waitTime);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            currentDisolveValue = Mathf.Lerp(start, target, progress);
            yield return null;
        }
        skeletonController.agentState = EnemyAgent.AgentStates.Dead;

        Destroy(gameObject);
        skeletonController.risen = false;
        //gameObject.SetActive(false);
    }
}
