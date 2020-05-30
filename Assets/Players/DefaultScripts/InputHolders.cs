using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHolders : MonoBehaviour
{
    //existir como hijo de playerPrefab y que cada UNO de los jugadres  tenga uno de estos objetos en el 

    public Vector2 movementInput = Vector2.zero;
    public Vector2 lookInput = Vector2.zero;
    public float attackInput = 0;
    public float ability1Input = 0;
    public float ability2Input = 0;
    public float ability3Input = 0;
    public float ability4Input = 0;
    public float pauseInput = 0;

    public void SetMoveInputVector(Vector2 move) => movementInput = move;
    public void SetLookInputVector(Vector2 look) => lookInput = look;
    public void SetAttackInputVector(float attack) => attackInput = attack;
    public void SetAbility1InputVector(float ability1) => ability1Input = ability1;
    public void SetAbility2InputVector(float ability2) => ability2Input = ability2;
    public void SetAbility3InputVector(float ability3) => ability3Input = ability3;
    public void SetAbility4InputVector(float ability4) => ability4Input = ability4;
    public void SetPauseMenu(float pause) => pauseInput = pause;
}
