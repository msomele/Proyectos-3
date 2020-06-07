using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSmashColliderFunction : MonoBehaviour
{
    public BarbarianController player;
    public LayerMask enemymask;

    public void OnTriggerEnter(Collider collision)
    {
        /*
        Debug.LogWarning(collision.gameObject.name);
        if (collision.gameObject.layer == enemymask)
        {
            
                if (collision.transform.GetComponent<SkeletonRagdoll>() && collision.transform.GetComponent<SkeletonController>().risen == true)
                {
                    SkeletonRagdoll skeleton = collision.transform.GetComponent<SkeletonRagdoll>();
                    if (skeleton != null)
                    {
                        skeleton.Die();
                    }
                    player.Explode();
                }


                if (collision.transform.GetComponent<LichController>())
                {
                    LichController lich = collision.transform.GetComponent<LichController>();
                    if (lich != null)
                    {
                        lich.TakeDamage(player.ab1AttackDmg);
                    }
                    player.Explode();
                }

                if (collision.transform.GetComponent<GolemController>())
                {
                    GolemController golem = collision.transform.GetComponent<GolemController>();
                    if (golem != null)
                    {
                        golem.TakeDamage(player.ab1AttackDmg);
                    }
                    player.Explode();
                }

           
        }
        */


    }
}
