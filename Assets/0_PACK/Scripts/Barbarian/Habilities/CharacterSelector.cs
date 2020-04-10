using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public CharacterClass[] characterClass;
    public Vector3 spawnPosition = new Vector3(0, 0, 0);


    /*This 2 gameobjects are for the panels in the UI where the players can choose which character use*/
    public GameObject characterSelectPanel;
    public GameObject abilityPanel; 

    public void StartGame(int characterChoice)
    {
        characterSelectPanel.SetActive(false);
        abilityPanel.SetActive(true);


        CharacterClass selectedCharacter = characterClass[characterChoice];
        /*Instantiates the player, though it only instantiates 1 of them... tweak this so instantiatin will
         occure when start game button pressed maybe? */
        GameObject spawnedPlayer = Instantiate(selectedCharacter.playerPrefab, spawnPosition, Quaternion.identity) as GameObject;
        WeaponMarker weaponMarker = GetComponentInChildren<WeaponMarker>();
        AbilityCooldown[] coolDownButtons = GetComponentsInChildren<AbilityCooldown>();

        for (int i = 0; i < coolDownButtons.Length; i++)
        {
            coolDownButtons[i].Initialize(selectedCharacter.characterAbilities[i], weaponMarker.gameObject);
        }
    }
} 
