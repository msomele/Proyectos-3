using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarbarianController : PlayerController 
 //Con heredar de PlayerController y en: Start, Update y FixedUpdate poner base.tatata() ya se mueve con el defaultSet.
{

    [HideInInspector] public Animator barbarianAnimator;
    public LayerMask enemyLayers;
    public LayerMask explosionLayers;

    public Transform attackPointMin, attackPointMax;

    [HideInInspector] public float attackRange = 0.5f;
    [HideInInspector] public float attackDamage = 20;
    [HideInInspector] public float attackRate = 2f; //nº of attacks/sg

    [HideInInspector] float nextAttackTime = 0f;
    [HideInInspector] float nextAbility1Time = 5f;
    [HideInInspector] float ability1Rate = 0.20f;
    [HideInInspector] int aux = 0;
    public AudioClip aa1;
    public AudioClip aa2;
    public GameObject hitPoint; 
    //-----------------------HEALING-------------------------------//
    public float baseHealingSpeed = 0.5f;
    public float timePassedSinceHitten = 0;
    public float maxTimeSinceHitten;
    public HealthRestoring hpRestoring;
    /************MAKE UPDATE TO hpRestoring.maxHealth WITH hp VARIABLE WHEN CHANGED **************/

    //-----------------------ABILITY1-------------------------------//

    public GameObject Ability1Collider;
    private float ability1ColliderTime = 3f;

    //-----------------------ABILITY2-------------------------------//

    [Range (0,140)] public float furyValue = 0;
    public float furyValueIncrement = 8.75f;
    public RectTransform furyBar;
    public float ab2HealingSpeed = 3.5f;
    public float healingIncreasedTime = 10f;


    public override void Start()
    {
        hpRestoring = GetComponent<HealthRestoring>();
        base.Start();
        barbarianAnimator = GetComponent<Animator>();
        maxTimeSinceHitten = 20f; 
        furyBar = GameObject.FindGameObjectWithTag("FuryP" + (GetPlayerIndex() + 1).ToString()).GetComponent<RectTransform>();
    }

    public override void Update()
    {
        base.Update();
        if (inputH.attackInput == 1)
        {
            Debug.Log(gameObject.name + ": I am attacking");
            if (Time.time >= nextAttackTime)
            {

                Attack();
                nextAttackTime = Time.time + 1f / attackRate;

            }
        }
        inputH.attackInput = 0;

        //hacer esto en el otro lao o que !!!!!!!debugear 

        timePassedSinceHitten += Time.deltaTime;

        
        if (timePassedSinceHitten >= maxTimeSinceHitten)
        {
            timePassedSinceHitten = maxTimeSinceHitten;
            hpRestoring.timePassedSinceHitten = timePassedSinceHitten;
        }
    }

    public override void FixedUpdate() 
    {
        base.FixedUpdate();
        
    }


    public override void Attack()
    {
        if(aux <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(aa1);
            barbarianAnimator.SetTrigger("BarbarianAA");
            aux++;
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(aa2);
            barbarianAnimator.SetTrigger("BarbarianAA2");
            aux = 0;
        }

        Collider[] hitten = Physics.OverlapCapsule(attackPointMin.position, attackPointMax.position, attackRange, enemyLayers); 
        foreach (Collider obj in hitten)
        {
            Debug.Log("Damage dealed to ->" + obj.name);

            if (obj.transform.GetComponent<SkeletonRagdoll>() && obj.transform.GetComponent<SkeletonController>().risen == true)
            {
                SkeletonRagdoll skeleton = obj.transform.GetComponent<SkeletonRagdoll>();
                if (skeleton != null)
                {
                    skeleton.Die();
                }
                Explode();
            }


            if (obj.transform.GetComponent<LichController>())
            {
                LichController lich = obj.transform.GetComponent<LichController>();
                if (lich != null)
                {
                    lich.TakeDamage(attackDamage);
                }
                Explode();
            }

            if (obj.transform.GetComponent<GolemController>())
            {
                GolemController golem = obj.transform.GetComponent<GolemController>();
                if (golem != null)
                {
                    golem.TakeDamage(attackDamage);
                }
                Explode();
            }

        }

        if(hitten.Length > 0)
            IncrementFury();

        if(!hpRestoring.isAbility)
            timePassedSinceHitten = 0;
        hpRestoring.timePassedSinceHitten = timePassedSinceHitten; //reset si ataco al healing
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPointMax == null || attackPointMin == null)
            return; 

        Gizmos.DrawWireSphere(attackPointMin.position, attackRange);
        Gizmos.DrawWireSphere(attackPointMax.position, attackRange);
        Gizmos.DrawLine(attackPointMin.position,attackPointMax.position);
    }

    public void HammerSmash() 
    {

        /* 
         * call from there doDmg() funcion.
         * 
         */

        barbarianAnimator.SetTrigger("HammerSmash");

            Debug.Log("Abilit1: HammerSmash used");

        if (!hpRestoring.isAbility)
            timePassedSinceHitten = 0;
        hpRestoring.timePassedSinceHitten = timePassedSinceHitten;

    }

    public void FuryHealing()
    {
        hpRestoring.isAbility = true;
        ResetFury();
        timePassedSinceHitten = maxTimeSinceHitten;
        hpRestoring.pointIncreasePerSecond = ab2HealingSpeed;
        StartCoroutine("WaitToSetHealing");
    }
    
    IEnumerator WaitToSetHealing()
    {
        yield return new WaitForSeconds(healingIncreasedTime);
        hpRestoring.pointIncreasePerSecond = baseHealingSpeed;
        hpRestoring.isAbility = false;

    }

    private void OnTriggerStay(Collider other) //this 2 do in the collider itself, call dmg from there as well!!
    {
        int i = 0; 
        if(other.gameObject.layer == enemyLayers)
        {
            Debug.Log("HammerSmashed: " + i +">" + other.gameObject.name);
            i++;
        }
    }






    /*EXTRA FUNCTIONS*/

    void IncrementFury()
    {
        furyValue += furyValueIncrement;
        if (furyValue > 140) furyValue = 140;
        furyBar.sizeDelta = new Vector2(furyValue, furyBar.sizeDelta.y);

    }
    void ResetFury()
    {
        furyValue = 0;
        furyBar.sizeDelta = new Vector2(furyValue, furyBar.sizeDelta.y);

    }
    void Explode()
    {
        Debug.LogWarning("explode");
        
        Collider[] hitten = Physics.OverlapCapsule(attackPointMin.position, attackPointMax.position, attackRange, enemyLayers);
        foreach(Collider bone in hitten)
        {
            Rigidbody rigidbody = bone.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(10000f, hitPoint.transform.position, 3f , 25f); //Normal
            }
        }
      
    }






}
