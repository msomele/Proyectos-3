using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianController : PlayerController 
 //Con heredar de PlayerController y en: Start, Update y FixedUpdate poner base.tatata() ya se mueve con el defaultSet.
{

    public Animator barbarianAnimator;
    public float lastClickedTime;
    public int noOfClicks;
    public float maxComboDelay;

    [SerializeField] bool isAttacking = false; 

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();   
        if(Time.time - lastClickedTime > maxComboDelay)
            noOfClicks = 0;

        //Debug.Log("Time: " + Time.time);
        
    }
    public override void FixedUpdate() 
    {
        base.FixedUpdate();
        AutoAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 8)
        {
            Debug.Log("I hitted " + other.gameObject.name);
            Destroy(other.gameObject);

        }
        
    }

    /*<Basic attack>*/
    #region Basic attack barbarian

    void GiveChanceToAttack()
    {
        if (controls.Gameplay.Attack.triggered)
        {
            isAttacking = true;     

        }
    }



    void AutoAttack()
    {
        if (controls.Gameplay.Attack.triggered)
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if(noOfClicks == 1)
            {
                barbarianAnimator.SetBool("Attack", true);
                barbarianAnimator.SetBool("Attack2", false);
                barbarianAnimator.SetBool("Attack3", false);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
        
    }

    public void ReturnAaOne()
    {
        if (noOfClicks >= 2)
        {
            barbarianAnimator.SetBool("Attack", false);
            barbarianAnimator.SetBool("Attack2", true);
            barbarianAnimator.SetBool("Attack3", false);

        }
        else
        {
            barbarianAnimator.SetBool("Attack", false);
            barbarianAnimator.SetBool("Attack2", false);
            barbarianAnimator.SetBool("Attack3", false);
            noOfClicks = 0;
        }

    }
    public void ReturnAaTwo()
    {
        if (noOfClicks >= 3)
        {
            barbarianAnimator.SetBool("Attack", false);
            barbarianAnimator.SetBool("Attack2", false);
            barbarianAnimator.SetBool("Attack3", true);
        }
        else
        {
            barbarianAnimator.SetBool("Attack", false);
            barbarianAnimator.SetBool("Attack2", false);
            barbarianAnimator.SetBool("Attack3", false);
            noOfClicks = 0;
        }

    }
    public void ReturnAaThree()
    {
        barbarianAnimator.SetBool("Attack", false);
        barbarianAnimator.SetBool("Attack2", false);
        barbarianAnimator.SetBool("Attack3", false);
        noOfClicks = 0;

    }

    #endregion






    void ConeDamageHab()
    {

    }
    void JumpAreaDamageHab()
    {

    }
    void ThrowWeaponHab()
    {

    }


}
