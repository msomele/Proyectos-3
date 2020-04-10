using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Abilities/HammerSmashAbility")]
public class HammerSmashAbility : Ability
{
    public int hammerDamage = 10;
    public float hammerRange = 3f;
    public float hitVel = 1f;

    private BarbarianController playerController;

    public override void Initialize(GameObject obj) //pasar como ref el obj que tenga el BarbarianController
    {
        //Actuate as the Start in monobehaviour, but from scriptableobjetcs.
        playerController = obj.GetComponent<BarbarianController>();
        playerController.Initialize();
        //set monobeh. script variables as the scriptableobjetc ones. 
        
    }

    public override void TriggerAbility()
    {
        //playerController.funciondelahabilidad();
    }
}
