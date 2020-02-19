using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

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
        
        Destroy(gameObject);
    }
    void explode()
    {
        Debug.Log("explode");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        
        foreach (Collider closeObjects in colliders)
        {
            Rigidbody rigidbody = closeObjects.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                //rigidbody.AddExplosionForce(2000f, transform.position, 50f, 50f); //Barbarian Jab
                rigidbody.AddExplosionForce(2000f, transform.position, 1f, 0.5f); //Normal
            }
        }
    }
}
