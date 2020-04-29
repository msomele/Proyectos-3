using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Barbarian/HealingAbility")]
public class HealingAbility : Ability
{
    public BarbarianController playerController;
    public float baseHealingSpeed = 0.5f;
    public float advancedHealingSpeed = 3.5f;
    public float healingIncreasedTime = 10f;
    public float maxTimeSinceHitten = 20f;
    public override void Initialize(GameObject obj)
    {
        playerController = obj.GetComponent<BarbarianController>();
        playerController.baseHealingSpeed = baseHealingSpeed;
        playerController.ab2HealingSpeed = advancedHealingSpeed;
        playerController.healingIncreasedTime = healingIncreasedTime;
        playerController.maxTimeSinceHitten = maxTimeSinceHitten;
    }
    public override void TriggerAbility()
    {
        playerController.FuryHealing();

    }

}
