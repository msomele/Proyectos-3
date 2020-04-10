using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianController : PlayerController 
 //Con heredar de PlayerController y en: Start, Update y FixedUpdate poner base.tatata() ya se mueve con el defaultSet.
{

    public Animator barbarianAnimator;
    public LayerMask enemyLayers;

    public Transform attackPointMin, attackPointMax;

    public float attackRange = 0.5f;
    public float attackDamage = 20;
    public float attackRate = 2f; //nº of attacks/sg

    float nextAttackTime = 0f;
    float nextAbility1Time = 5f;
    float ability1Rate = 0.20f;

    //-----------------------ABILITY1-------------------------------//

    public GameObject Ability1Collider;




    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (attackInput == 1)
        {
            Debug.Log(gameObject.name + ": I am attacking");
            if (Time.time >= nextAttackTime)
            {

                Attack();
                nextAttackTime = Time.time + 1f / attackRate;

            }
        }
        attackInput = 0;

        if (ability1Input == 1)
        {
            Debug.LogWarning("ability1 used");
            if(Time.time >= nextAbility1Time)
            {
                HammerSmash();
                nextAbility1Time = Time.time + 1f / ability1Rate;
            }
        }
        else {  Ability1Collider.GetComponent<MeshCollider>().enabled = false; }
        ability1Input = 0;
    }

    public override void FixedUpdate() 
    {
        base.FixedUpdate();
        
    }


    public override void Attack()
    {
        //set attack animation
            Collider[] hitten = Physics.OverlapCapsule(attackPointMin.position, attackPointMax.position, attackRange, enemyLayers); //Con rampas cuidao, quizás cambiarlo a raycast?
            foreach (Collider obj in hitten)
            {
                Debug.Log("Damage dealed to ->" + obj.name);
                //obj.GetComponent<> >> TakeDamage( ): 

            }

        
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointMax == null || attackPointMin == null)
            return; 

        Gizmos.DrawWireSphere(attackPointMin.position, attackRange);
        Gizmos.DrawWireSphere(attackPointMax.position, attackRange);
        Gizmos.DrawLine(attackPointMin.position,attackPointMax.position);
    }

    public void HammerSmash() //maybe coroutine needed, for trigger 2 stay while anim is playing... or another f() 2 desable collider when anim ends
    {
            Ability1Collider.GetComponent<MeshCollider>().enabled = true;
            //activar solo el collider que quiero, el call se hará desde ontriggerenter. llamar a esta funcion desde animacion en unity !
            Debug.Log("Abilit1: HammerSmash used");
        

        
    }

    private void OnTriggerStay(Collider other)
    {
        int i = 0; 
        if(other.gameObject.layer == enemyLayers)
        {
            Debug.Log("HammerSmashed: " + i +">" + other.gameObject.name);
            i++;
        }
    }
}
