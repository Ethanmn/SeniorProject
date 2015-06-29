using UnityEngine;
using System.Collections;
using System;

public class PlayerStateFlinch : I_PlayerState {

    private Transform enemy;

    private float speed;
    private float timer;

    private PlayerStats stats;

    // Knockback direction
    private Vector2 kbDir;

    public PlayerStateFlinch(Transform enemy)
    {
        this.enemy = enemy;
    }

    void I_PlayerState.OnEnter(Transform player)
    {
        player.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[4];

        stats = player.GetComponent<PlayerStats>();
        stats.Flinching = true;

        speed = 4.0f;
        timer = stats.FlinchTimer / 2;

        kbDir = (player.position - enemy.position).normalized;
    }

    void I_PlayerState.OnExit(Transform player)
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    I_PlayerState I_PlayerState.Update(Transform player, float dt)
    {
        player.GetComponent<Rigidbody2D>().velocity = kbDir * speed;

        // Done flinching
        if (timer <= 0)
        {
            
            return new PlayerStateIdle();
        }

        timer -= dt;

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
