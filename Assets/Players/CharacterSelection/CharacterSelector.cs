using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSelector : MonoBehaviour
{   
    public CharacterClass[] characters;
    public Vector3 spawnP1Position = new Vector3(0, 0, 0);
    public Vector3 spawnP2Position = new Vector3(0, 0, 0);
    

   private void Awake() => DontDestroyOnLoad(this); //Comprobar si es este objeto el que no debe ser destruido, o el Canvas entero (!Ojo!)
    

    /*This 2 gameobjects are for the panels in the UI where the players can choose which character use*/


    public void StartGame(int characterChoice, GameObject player)
    {
        CharacterClass selectedCharacter = characters[characterChoice];
        Debug.Log(selectedCharacter.name +":::>>"+characterChoice); 
        /*Instantiates the player, though it only instantiates 1 of them... tweak this so instantiatin will
         occure when start game button pressed maybe? */
        GameObject spawnedPlayer = Instantiate(selectedCharacter.playerPrefab, spawnP1Position, Quaternion.identity) as GameObject; //playerprefab que sea lo usado en inputsystem pa instanciar!!!!!!
        WeaponMarker weaponMarker = spawnedPlayer.GetComponentInChildren<WeaponMarker>(); // not sure if needed 
        AbilityCooldown[] coolDownButtons = player.GetComponentsInChildren<AbilityCooldown>();

        Debug.Log(coolDownButtons.Length);
        for (int i = 0; i < coolDownButtons.Length; i++)
        {
            Debug.Log(coolDownButtons[i].name + coolDownButtons[i].gameObject.GetComponentInParent<GameObject>().name);
            coolDownButtons[i].Initialize(selectedCharacter.characterAbilities[i], weaponMarker.gameObject);
        }
    }
} 
