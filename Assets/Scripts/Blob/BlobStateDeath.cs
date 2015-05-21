﻿using UnityEngine;
using System.Collections;
using System;

public class BlobStateDeath : I_MobState
{
    private float timer;
    private int blinkCount;
    private bool blink;

    void I_MobState.OnEnter(Transform mob, MobStats stats)
    {
        timer = 0.3f;
        blinkCount = 0;
        blink = false;
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void I_MobState.OnExit(Transform mob)
    {
        
    }

    I_MobState I_MobState.Update(Transform mob, float dt)
    {
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (blink)
        {
            mob.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        }
        else
        {
            mob.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

        if (timer <= 0)
        {
            GameObject.Destroy(mob.gameObject);
        }

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
    I_MobState I_MobState.FixedUpdate(Transform mob, float dt)
    {
        return null;
    }
    I_MobState I_MobState.HandleInput(Transform mob)
    {
        return null;
    }

    I_MobState I_MobState.OnCollisionEnter(Transform mob, Collision2D c)
    {
        return null;
    }

}
