using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestoring : MonoBehaviour
{
    public float updatedHealth;
    public float maxHealth;
    public float pointIncreasePerSecond;


    public float timePassedSinceHitten;
    public float maxTimeSinceHitten;

    public RectTransform hpVisual;
    private BarbarianController player;    
    private void Start()
    {
        player = this.GetComponent<BarbarianController>();
        maxHealth = player.hp;
        pointIncreasePerSecond = player.baseHealingSpeed;
        timePassedSinceHitten = player.timePassedSinceHitten;
        maxTimeSinceHitten = player.maxTimeSinceHitten;
        updatedHealth = maxHealth;
    }
    private void Update()
    {
    
        if(updatedHealth > maxHealth) updatedHealth = maxHealth;
        if (updatedHealth < 0) updatedHealth = 0;
        
        if(timePassedSinceHitten >= maxTimeSinceHitten)
        {
            updatedHealth += pointIncreasePerSecond * Time.deltaTime;
            hpVisual.sizeDelta = new Vector2(updatedHealth, hpVisual.sizeDelta.y);
        }
    }
}
