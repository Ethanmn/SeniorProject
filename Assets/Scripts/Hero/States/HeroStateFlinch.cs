﻿using UnityEngine;
using System.Collections;
using System;

public class HeroStateFlinch : I_HeroState {

    private Transform enemy;

    private float speed;
    private float timer;

    private HeroStats stats;

    // Knockback direction
    private Vector2 kbDir;

    public HeroStateFlinch(Transform enemy)
    {
        this.enemy = enemy;
    }

    void I_HeroState.OnEnter(Transform hero)
    {
        hero.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[4];

        stats = hero.GetComponent<HeroStats>();
        stats.Flinching = true;

        // Speed at which the hero is knocked back
        speed = 4.0f;
        // How long the hero should be hit back
        timer = stats.FlinchTimerRaw / 5;

        // Find the direction of the knockback
        kbDir = (hero.position - enemy.position).normalized;
    }

    void I_HeroState.OnExit(Transform hero)
    {
        hero.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    I_HeroState I_HeroState.Update(Transform hero, float dt)
    {
        hero.GetComponent<Rigidbody2D>().velocity = kbDir * speed;

        // Done flinching
        if (timer <= 0)
        {
            return new HeroStateIdle();
        }

        timer -= dt;

        return null;
    }

    I_HeroState I_HeroState.HandleInput(Transform hero)
    {
        return null;
    }

    I_HeroState I_HeroState.OnCollisionEnter(Transform hero, Collision2D c)
    {
        return null;
    }
}
