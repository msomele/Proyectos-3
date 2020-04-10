using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCooldown : MonoBehaviour
{
    //este script se asigna al boton de la ui//

    public string abilityButtonAxisName = ""; //que boton triggers la ability
    public Image darkMask;
    public TMP_Text coolDownTextDisplay;

    private Ability ability; //QUITAR ESTO MAS TARDE 
    private GameObject weaponHolder; //quitar
    private Image myButtonImage;
    private AudioSource abilitySource;
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    private void Start()
    {
        //Initialize(ability,weaponHolder);
        
    }

    public void Initialize(Ability selectedAbility, GameObject weaponHolder)
    {
        ability = selectedAbility;
        myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        myButtonImage.sprite = ability.aSprite;
        darkMask.sprite = ability.aSprite;
        coolDownDuration = ability.aBaseCd;
        ability.Initialize(weaponHolder);
        AbilityReady();
    }

    private void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if(coolDownComplete)
        {
            AbilityReady();
            if(Input.GetButtonDown(abilityButtonAxisName)) //cambiar x nuevo input
            {
                ButtonTriggered();
            }
        }
        else
        {
            CoolDown();
        }
    }

    private void AbilityReady()
    {
        coolDownTextDisplay.enabled = false;
        darkMask.enabled = false; 
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        coolDownTextDisplay.text = roundedCd.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    private void ButtonTriggered()
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        darkMask.enabled = true;
        coolDownTextDisplay.enabled = true;
        abilitySource.clip = ability.aSound;
        abilitySource.Play();
        ability.TriggerAbility();
    }

}
