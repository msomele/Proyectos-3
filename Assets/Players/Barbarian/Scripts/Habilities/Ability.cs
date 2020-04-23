using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject  //abstracta porque funciones que tiene no tienen implementacion, otras clases necesitan funciones de aquí
{
    public string aName = "";
    public Sprite aSprite;
    public AudioClip aSound;

    public float aBaseCd = 1f;

    
    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();

}
