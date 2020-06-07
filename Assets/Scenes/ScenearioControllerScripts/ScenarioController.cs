using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System;

public class ScenarioController : MonoBehaviour
{
    public GameObject[] ObjectivesArray;
    public EnemyPull[] EnemyPulls;
    public float gameTime;
    public bool gameEnded;
    public TMP_Text timePasedText;
    private int starsEarned;
    public GameObject[] starsGameObject;
    public GameObject VictoryText;
    public GameObject DefeatText;
    public GameObject FinalUI;
    public BBDDconnection bbdd;

    private bool bbddConnection; 

    private void Start()
    {
        bbddConnection = false; 
        Time.timeScale = 1f;
        for (int i = 0; i < starsGameObject.Length-1; i++)
        {
            starsGameObject[i].SetActive(false);
        }
        gameEnded = false;
        gameTime = 0f;
    }

    private void Update()
    {
        if (ObjectivesArray[4].GetComponent<DestructibleObjective>().isDestroyed == true)
        {
            gameEnded = true;
        }
        if (!gameEnded)
        {
            gameTime += Time.deltaTime;
        }
        else
        {
            FinalUI.SetActive(true);
            starsEarned = 0;
            if (ObjectivesArray[4].GetComponent<DestructibleObjective>().isDestroyed == false)
            {
                starsEarned += 1;
            }
            if (ObjectivesArray[3].GetComponent<DestructibleObjective>().isDestroyed == false)
            {
                starsEarned += 1;
            }
            if (ObjectivesArray[0].GetComponent<DestructibleObjective>().isDestroyed == false)
            {
                starsEarned += 1;
            }
            for (int i = 0; i < starsEarned; i++)
            {
                if (!starsGameObject[i].activeSelf)
                {
                    starsGameObject[i].SetActive(true);
                }
                
            }
            if (starsEarned > 0)
            {
                VictoryText.SetActive(true);
                DefeatText.SetActive(false);
            }
            else
            {
                VictoryText.SetActive(false);
                DefeatText.SetActive(true);
            }

            timePasedText.text = ConvertSecondsToTimeString(gameTime);
            if(!bbddConnection)
            {
                bbdd.UpdateDatabase(starsEarned, ConvertSecondsToTimeString(gameTime));
                bbddConnection = true; 
            }
        }
        
    }
    private string ConvertSecondsToTimeString(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string str = time.ToString(@"hh\:mm\:ss");
        return str;
    }

    void Awake()
    {
        TransferObjectivesToPulls();
    }

    void TransferObjectivesToPulls()
    {
        //Debug.Log(EnemyPulls.Length);
        for (int i = 0; i < EnemyPulls.Length; i++)
        {
            if (EnemyPulls[i] != null)
            {
                for (int j = 0; j < this.ObjectivesArray.Length; j++)
                {
                    EnemyPulls[i].ObjectivesArray[j] = this.ObjectivesArray[j];
                }
            }
        }
    }
}
