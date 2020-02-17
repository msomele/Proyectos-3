using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianController : PlayerController 
 //Con heredar de PlayerController y en: Start, Update y FixedUpdate poner base.tatata() ya se mueve con el defaultSet.
{

    public Animator barbarianAnimator; 

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();   
    }
    new void FixedUpdate() 
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

    void AutoAttack()
    {
        if (controls.Gameplay.Attack.triggered)
        {
            barbarianAnimator.SetBool("Attack", true);
        }
        else
            barbarianAnimator.SetBool("Attack", false);
    }
    void StopAutoAttack() => barbarianAnimator.SetBool("Attack", false);

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
