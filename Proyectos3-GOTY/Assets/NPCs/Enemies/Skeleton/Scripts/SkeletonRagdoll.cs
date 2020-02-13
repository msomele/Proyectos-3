using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonRagdoll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setRigidBodyState(true);
        setCollidersState(false);
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
}
