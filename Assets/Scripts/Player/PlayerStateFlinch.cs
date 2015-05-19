using UnityEngine;
using System.Collections;
using System;

public class PlayerStateFlinch : I_PlayerState {

    private Transform enemy;

    private float speed;
    private float timer;
    private int blinkCount;
    private bool blink;

    public PlayerStateFlinch(Transform enemy)
    {
        this.enemy = enemy;
    }

    void I_PlayerState.OnEnter(Transform player)
    {
        player.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[4];

        speed = 3.0f;
        timer = 0.3f;
        blinkCount = 0;
        blink = false;
    }

    void I_PlayerState.OnExit(Transform player)
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    I_PlayerState I_PlayerState.Update(Transform player, float dt)
    {
        if (timer > timer / 2f)
        {
            player.GetComponent<Rigidbody2D>().velocity = (player.position - enemy.position).normalized * speed;
        }
        else
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        
        if (blink)
        {
            player.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        }
        else
        {
            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

        // Done flinching
        if (timer <= 0)
        {
            return new PlayerStateIdle();
        }

        // Make the player flash when flinching
        if (blinkCount == 4)
        {
            blink = !blink;
            blinkCount = 0;
        }
        else
            blinkCount++;

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
