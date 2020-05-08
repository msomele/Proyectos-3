using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Character")]
public class CharacterClass : ScriptableObject
{
    public string characterName = "Default";
    public int startingHp = 100;
    public float startingDmg = 10;

    public float attackRange = 0.5f;
    public float attackDamage = 20;
    public float attackRate = 2f;

    public float startomgHealthRegen = 0.5f;

    public Ability[] characterAbilities;
    public GameObject playerPrefab;



}


/* 
 * This class is the character default class. It gets the character prefab, and their set of abilities
 * then in characterselector script will be an array of chacarter classes, being all the possible characters
 * 2 play. 
 * 
 * */