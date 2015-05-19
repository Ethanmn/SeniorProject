using UnityEngine;
using System.Collections;
using System;

public class PlayerStateDeath : I_PlayerState {

    void I_PlayerState.OnEnter(Transform player)
    {
        
    }

    void I_PlayerState.OnExit(Transform player)
    {
        
    }

    I_PlayerState I_PlayerState.Update(Transform player, float dt)
    {
        GameObject.Destroy(player.gameObject);
        return null;
    }

    I_PlayerState I_PlayerState.HandleInput(Transform player)
    {
        return null;
    }

    I_PlayerState I_PlayerState.OnCollisionEnter(Transform player, Collision2D c)
    {
        return null;
    }

}
