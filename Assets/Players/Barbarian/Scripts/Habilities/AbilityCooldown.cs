using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCooldown : MonoBehaviour
{
    /*este script se asigna al boton de la ui*/
    [SerializeField] private string abilityName;
    public Image darkMask;
    public TMP_Text coolDownTextDisplay;
    public InputHolders holder;

    [SerializeField] //QUITAR ESTO MAS TARDE 
    private Ability ability;
    [SerializeField] //quitar
    private GameObject aCWeaponHolder; 

    private Image myButtonImage;
    private AudioSource abilitySource;

    public bool startsInReady;


    public float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    private Image[] aux = new Image[2];
    private void Start()
    {/*
        abilityName = ability.aName;
        Initialize(ability,weaponHolder);
        SelectInput(abilityName); */
        myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        aux = GetComponentsInChildren<Image>();
        darkMask = aux[1];
        coolDownTextDisplay = GetComponentInChildren<TMP_Text>();
    }

    public void Initialize(Ability selectedAbility, GameObject weaponHolder)
    {
        ability = selectedAbility;
        abilityName = ability.aName;

        aCWeaponHolder = weaponHolder;
        holder = aCWeaponHolder.GetComponentInChildren<InputHolders>();
        
        coolDownDuration = ability.aBaseCd;
        Debug.Log("A base cD: " + ability.aBaseCd);
        /*
        myButtonImage.sprite = ability.aSprite;
        darkMask.sprite = ability.aSprite;*/
        ability.Initialize(aCWeaponHolder);
        if (startsInReady)
            AbilityReady();
        SelectInput(abilityName);
    }

    private void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if(coolDownComplete)
        {
            AbilityReady();
            SelectInput(abilityName);
        }
        else
        {
            CoolDown();
        }
    }

    private void AbilityReady()
    {
        coolDownTextDisplay.enabled = false; //!
        darkMask.enabled = false; 
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        coolDownTextDisplay.text = roundedCd.ToString();
        darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration); //% of the cooldown duration left, used to elapse the mask
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


    private void SelectInput(string text) //pasar el nombre de la habilidad
    {
        switch(text)
        {
            case "HammerSmash":
                if (holder.ability1Input > 0)
                {
                    ButtonTriggered();
                }
                break;
            case "Healing":
                if (holder.ability2Input > 0 && aCWeaponHolder.GetComponent<BarbarianController>().furyValue >= 140)
                {
                    ButtonTriggered();
                }
                break;
        }
    }

}
