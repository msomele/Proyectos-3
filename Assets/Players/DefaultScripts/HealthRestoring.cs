using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthRestoring : MonoBehaviour
{
    public float updatedHealth;
    public float maxHealth;
    public float pointIncreasePerSecond;


    public float timePassedSinceHitten;
    public float maxTimeSinceHitten;
    public bool isAbility;
    public RectTransform hpVisual;
    private BarbarianController player;

    public ScenarioController scenarioController;

    private void Start()
    {
        player = this.GetComponent<BarbarianController>();
        hpVisual = GameObject.FindGameObjectWithTag("HpP" + (player.GetPlayerIndex()+1).ToString()).GetComponent<RectTransform>();

        isAbility = false;
        maxHealth = player.hp;
        pointIncreasePerSecond = player.baseHealingSpeed;
        timePassedSinceHitten = player.timePassedSinceHitten;
        maxTimeSinceHitten = player.maxTimeSinceHitten;
        updatedHealth = maxHealth;
        scenarioController = GameObject.FindObjectOfType<ScenarioController>();

    }
    private void Update()
    {
            hpVisual.GetComponent<Image>().fillAmount = updatedHealth / maxHealth;
           // hpVisual.sizeDelta = new Vector2(updatedHealth/2, hpVisual.sizeDelta.y);

        if (updatedHealth >= maxHealth) updatedHealth = maxHealth;
        if (updatedHealth <= 0)
        {
            updatedHealth = 0;
            hpVisual.GetComponent<Image>().fillAmount = updatedHealth / maxHealth;
            scenarioController.PlayerDied = true; 
            //hpVisual.sizeDelta = new Vector2(updatedHealth / 2, hpVisual.sizeDelta.y);
        }
        //default health regeneration
        if(timePassedSinceHitten >= maxTimeSinceHitten)
        {
            updatedHealth += pointIncreasePerSecond * Time.deltaTime;
            hpVisual.GetComponent<Image>().fillAmount = updatedHealth / maxHealth;
            //hpVisual.sizeDelta = new Vector2(updatedHealth / 2, hpVisual.sizeDelta.y);
        }
    }
}
