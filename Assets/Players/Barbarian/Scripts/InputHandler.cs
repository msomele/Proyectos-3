using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputHandler : MonoBehaviour
{
    private PlayerController player;
    private PlayerInput playerInput;
    private InputHolders holder;

    private void Awake()
    {
        playerInput = this.GetComponent<PlayerInput>();
        var index = playerInput.playerIndex;



        var players = FindObjectsOfType<PlayerController>();
        player = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
        Debug.Log(player.name + ": " + player.GetPlayerIndex());


        holder = player.GetComponentInChildren<InputHolders>();



    }

    public void OnMove(CallbackContext context)
    {
        if(player != null)
            holder.SetMoveInputVector(context.ReadValue<Vector2>());
    }
    public void OnLook(CallbackContext context)
    {
        if (player != null)
            holder.SetLookInputVector(context.ReadValue<Vector2>());
    }
    public void OnAttack(CallbackContext context)
    {
        if (player != null)
            holder.SetAttackInputVector(context.ReadValue<float>());
    }
    public void OnAbility1(CallbackContext context)
    {
        if (player != null)
            holder.SetAbility1InputVector(context.ReadValue<float>());

    }
    public void OnAbility2(CallbackContext context)
    {
        if (player != null)
            holder.SetAbility2InputVector(context.ReadValue<float>());

    }
    public void OnAbility3(CallbackContext context)
    {
        if (player != null)
            holder.SetAbility3InputVector(context.ReadValue<float>());

    }
    public void OnAbility4(CallbackContext context)
    {
        if (player != null)
            holder.SetAbility4InputVector(context.ReadValue<float>());

    }
    public void OnPauseMenu(CallbackContext context)
    {
        if (player != null)
            holder.SetPauseMenu(context.ReadValue<float>());

    }
}
