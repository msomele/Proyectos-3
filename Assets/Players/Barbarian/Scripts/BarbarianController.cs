using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarbarianController : PlayerController 
 //Con heredar de PlayerController y en: Start, Update y FixedUpdate poner base.tatata() ya se mueve con el defaultSet.
{
    public Animator barbarianAnimator;
    public LayerMask enemyLayers;
    public LayerMask explosionLayers;

    public Transform attackPointMin, attackPointMax;
    public Transform attackPointMin_abi1, attackPointMax_abi1, distance_abi1;
    public bool isIdle;
    public bool isAtt1;
    public bool isAtt2;
    [HideInInspector] public float attackRange = 0.5f;
    public float attackDamage = 20;
    [HideInInspector] public float attackRate = 2f; //nº of attacks/sg

    [HideInInspector] float nextAttackTime = 0f;
    [HideInInspector] float nextAbility1Time = 5f;
    [HideInInspector] float ability1Rate = 0.20f;
    public int aux = 0;//[HideInInspector]
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
    public float ab1AttackDmg = 0f;
    private float ability1ColliderTime = 3f;

    //-----------------------ABILITY2-------------------------------//

    [Range (0,140)] public float furyValue = 0;
    public float furyValueIncrement = 8.75f;
    public RectTransform furyBar;
    public float ab2HealingSpeed = 3.5f;
    public float healingIncreasedTime = 10f;


    public override void Start()
    {
        inputH = GetComponentInChildren<InputHolders>();
        hpRestoring = GetComponent<HealthRestoring>();
        base.Start();
        barbarianAnimator = this.GetComponentInChildren<Animator>();
        maxTimeSinceHitten = 20f; 
        furyBar = GameObject.FindGameObjectWithTag("FuryP" + (GetPlayerIndex() + 1).ToString()).GetComponent<RectTransform>();
        Ability1Collider.GetComponent<HammerSmashColliderFunction>().player = gameObject.GetComponent<BarbarianController>();
        Ability1Collider.GetComponent<HammerSmashColliderFunction>().enemymask = enemyLayers;

}

    public override void Update()
    {
        base.Update();
        if (inputH.attackInput == 1)
        {
            //Debug.Log(gameObject.name + ": I am attacking");
            if (Time.time >= nextAttackTime)
            {
                if (!isIdle && isAtt1)
                    SaveRotation();//previousRotation = rotation.transform.rotation;
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;

            }
        }
        inputH.attackInput = 0;
        

        timePassedSinceHitten += Time.deltaTime;

        
        if (timePassedSinceHitten >= maxTimeSinceHitten)
        {
            timePassedSinceHitten = maxTimeSinceHitten;
            hpRestoring.timePassedSinceHitten = timePassedSinceHitten;
        }
    }

    public override void FixedUpdate()
    {
        Move(barbarianAnimator);
        if (barbarianAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            aux = 0;
    }

    public override void Move(Animator player)
    {
        base.Move(barbarianAnimator);
    }

    public override void Attack()
    {
        barbarianAnimator.SetBool("Running", false);
        base.Attack();

        if(aux <= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(aa1);
            barbarianAnimator.SetTrigger("Attack");
            aux = 1;
            doubleAttack = true;
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(aa2);
            barbarianAnimator.SetTrigger("Attack2");
            aux = 0;
            doubleAttack = false;
        }

        if(!hpRestoring.isAbility)
            timePassedSinceHitten = 0;
        hpRestoring.timePassedSinceHitten = timePassedSinceHitten; //reset si ataco al healing

        //, 1.3f

    }
    public void TriggerDmgAAAction()
    {
        bool shouldFury = false;
        Collider[] hitten = Physics.OverlapCapsule(attackPointMin.position, attackPointMax.position, attackRange, enemyLayers);
        foreach (Collider obj in hitten)
        {
            //Debug.Log("Damage dealed to ->" + obj.name);

            if (obj.transform.GetComponent<SkeletonRagdoll>() && obj.transform.GetComponent<SkeletonController>().risen == true)
            {
                SkeletonRagdoll skeleton = obj.transform.GetComponent<SkeletonRagdoll>();
                if (obj.transform.GetComponent<SkeletonController>().agentState != EnemyAgent.AgentStates.Ragdolled)
                    shouldFury = true;
                else
                    shouldFury = false;

                if (skeleton != null)
                {
                    skeleton.Die();
                }
                Explode();
            }

            if (obj.transform.GetComponent<LichController>())
            {
                shouldFury = true;
                obj.transform.GetComponent<LichController>().TakeDamage(attackDamage);
            }

            if (obj.transform.GetComponent<GolemController>())
            {
                shouldFury = true;
                obj.transform.GetComponent<GolemController>().TakeDamage(attackDamage);
            }

        }

        if (hitten.Length >= 0 && shouldFury)
            IncrementFury();


    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointMax == null || attackPointMin == null || attackPointMin_abi1 == null || attackPointMax_abi1 == null)
            return;

        Gizmos.DrawWireSphere(attackPointMin.position, attackRange);
        Gizmos.DrawWireSphere(attackPointMax.position, attackRange);
        Gizmos.DrawLine(attackPointMin.position, attackPointMax.position);
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPointMin_abi1.position, 4);
        Gizmos.DrawWireSphere(attackPointMax_abi1.position, 4);
    }
    public void HammerSmash()
    {
       
        barbarianAnimator.SetBool("Running", false);
        RotateTowardsPointer();
        barbarianAnimator.SetTrigger("HammerSmash");
       

        if (!hpRestoring.isAbility)
            timePassedSinceHitten = 0;
        hpRestoring.timePassedSinceHitten = timePassedSinceHitten;

    }
    public void TriggerDmgAb1Action()
    {
        Physics.SyncTransforms();
        
        Collider[] hitten = Physics.OverlapCapsule(attackPointMin_abi1.position, attackPointMax_abi1.position, 4 , enemyLayers);
        bool shouldFury = false;
        //Collider[] hitten = Physics.OverlapCapsule(attackPointMin_abi1.position, attackPointMax_abi1.position, 3.5f, enemyLayers); //Vector3.Distance(this.gameObject.transform.position, distance_abi1.position)
        foreach (Collider obj in hitten)
        {
            //Debug.Log("Damage dealed to ->" + obj.name);

            if (obj.transform.GetComponent<SkeletonRagdoll>() && obj.transform.GetComponent<SkeletonController>().risen == true)
            {
                SkeletonRagdoll skeleton = obj.transform.GetComponent<SkeletonRagdoll>();
                if (obj.transform.GetComponent<SkeletonController>().agentState != EnemyAgent.AgentStates.Ragdolled)
                    shouldFury = true;
                else
                    shouldFury = false;

                if (skeleton != null)
                {
                    skeleton.Die();
                }
                Explode();
            }

            if (obj.transform.GetComponent<LichController>())
            {
                shouldFury = true;
                obj.transform.GetComponent<LichController>().TakeDamage(ab1AttackDmg);
            }

            if (obj.transform.GetComponent<GolemController>())
            {
                shouldFury = true;
                obj.transform.GetComponent<GolemController>().TakeDamage(ab1AttackDmg);
            }

        }

        if (hitten.Length >= 0 && shouldFury)
            IncrementFury();

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
    public void Explode()
    {
        //Debug.LogWarning("explode");
        
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
