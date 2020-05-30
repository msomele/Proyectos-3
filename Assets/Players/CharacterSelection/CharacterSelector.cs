using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSelector : MonoBehaviour
{   
    public CharacterClass[] characters;
    public Vector3 spawnP1Position = new Vector3(53 , 1 , 85 );
    public Vector3 spawnP2Position = new Vector3(53 , 1 , 80);
    public WeaponMarker weaponMarker1;
    public WeaponMarker weaponMarker2;

    private void Awake()
    {
            DontDestroyOnLoad(this); //Comprobar si es este objeto el que no debe ser destruido, o el Canvas entero (!Ojo!)
    }

    /*This 2 gameobjects are for the panels in the UI where the players can choose which character use*/

    private int auxi = 0;
    public void StartGame(int characterChoice, GameObject player)
    {
        CharacterClass selectedCharacter = characters[characterChoice];

        /*Instantiates the player, though it only instantiates 1 of them... tweak this so instantiatin will
         occure when start game button pressed maybe? */
       
        if (auxi == 0)
        {
            GameObject spawnedPlayer = Instantiate(selectedCharacter.playerPrefab, spawnP1Position, Quaternion.identity) as GameObject; //playerprefab que sea lo usado en inputsystem pa instanciar!!!!!!
            spawnedPlayer.GetComponent<PlayerController>()._playerIndex = 0;
            weaponMarker1 = spawnedPlayer.GetComponentInChildren<WeaponMarker>(); // Sirve para referenciar a la instancia del jugador

        }
        if (auxi >= 1)
        {
            GameObject spawnedPlayer = Instantiate(selectedCharacter.playerPrefab, spawnP2Position, Quaternion.identity) as GameObject; //playerprefab que sea lo usado en inputsystem pa instanciar!!!!!!
            spawnedPlayer.GetComponent<PlayerController>()._playerIndex = 1;
            weaponMarker2 = spawnedPlayer.GetComponentInChildren<WeaponMarker>();

        }

        AbilityCooldown[] coolDownButtons = player.GetComponentsInChildren<AbilityCooldown>(); //los botones de las habilidades
        if (auxi == 0)
        {
            for (int i = 0; i < coolDownButtons.Length; i++)
            {
                coolDownButtons[i].Initialize(selectedCharacter.characterAbilities[i], weaponMarker1.gameObject); //inicializar dichos botones
            }

        }
        if (auxi >= 1)
        {
            for (int i = 0; i < coolDownButtons.Length; i++)
            {
                coolDownButtons[i].Initialize(selectedCharacter.characterAbilities[i], weaponMarker2.gameObject); //inicializar dichos botones
            }

        }

        auxi++;
    }
} 
