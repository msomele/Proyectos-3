using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonRagdoll : MonoBehaviour
{
    private float currentDisolveValue;
    public Material dissolveMaterial;
    SkinnedMeshRenderer[] childrenRenderer;
    // Start is called before the first frame update
    void Start()
    {
        dissolveMaterial.SetFloat("Vector1_FEFF47F1", 0f);
        currentDisolveValue = 0f;
        childrenRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var r in childrenRenderer)
        {
            r.material.SetFloat("Dissolve", currentDisolveValue);
        }
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
        /*
        SkeletonRagdoll skeleton = hit.transform.GetComponent<SkeletonRagdoll>();
        if (skeleton != null)
        {
            skeleton.die();
        }
        */
        StartCoroutine(Disolve(5f));
        setCollidersState(true);
        setRigidBodyState(false);
        GetComponent<Animator>().enabled = false;
        GetComponent<SkeletonController>().enabled = false;
        GetComponent<SkeletonAnimationController>().enabled = false;
        GetComponent<AgentLinkMover>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
    }

    void setRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void setCollidersState(bool state)
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
        Destroy(gameObject);
    }
}
