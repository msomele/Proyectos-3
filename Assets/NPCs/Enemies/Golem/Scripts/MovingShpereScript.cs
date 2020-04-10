using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingShpereScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<SkeletonRagdoll>() && collision.transform.GetComponent<SkeletonController>().risen == true)
        {
            SkeletonRagdoll skeleton = collision.transform.GetComponent<SkeletonRagdoll>();
            if (skeleton != null)
            {
                skeleton.Die();
            }
            explode();
        }
    }

    void explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        Debug.Log("Quita bicho");
        foreach (Collider closeObjects in colliders)
        {
            Rigidbody rigidbody = closeObjects.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                //rigidbody.AddExplosionForce(2000f, transform.position, 50f, 50f); //Barbarian Jab
                rigidbody.AddExplosionForce(100f, transform.position, 1f, 0.5f); //Normal
            }
        }
    }
}
