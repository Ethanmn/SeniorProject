using UnityEngine;
using System.Collections;
using System;

public class PlayerStateFlinch : I_PlayerState {

    private Transform enemy;

    private float speed;
    private float timer;
    private int blinkCount;
    private bool blink;

    private PlayerStats stats;

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
        blinkCount = 0;
        blink = false;
    }

    void I_PlayerState.OnExit(Transform player)
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    I_PlayerState I_PlayerState.Update(Transform player, float dt)
    {
        player.GetComponent<Rigidbody2D>().velocity = (player.position - enemy.position).normalized * speed;

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
