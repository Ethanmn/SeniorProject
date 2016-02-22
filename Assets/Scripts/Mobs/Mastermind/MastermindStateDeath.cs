using UnityEngine;
using System.Collections;
using System;

public class MastermindStateDeath : I_MobState
{
    private float timer;
    private int blinkCount;
    private bool blink;
    private MastermindStats stats;

    void I_ActorState.OnEnter(Transform mob)
    {
        timer = mob.GetComponent<MobStats>().deathTimer;
        blinkCount = 0;
        blink = false;
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        stats = mob.GetComponent<MastermindStats>();
    }

    void I_ActorState.OnExit(Transform mob)
    {
        
    }

    I_ActorState I_ActorState.Update(Transform mob, float dt)
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
            // Signal that it died
            PublisherBox.onKillPub.RaiseEvent(mob);
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
    I_ActorState I_ActorState.HandleInput(Transform mob)
    {
        return null;
    }

    I_ActorState I_ActorState.OnCollisionStay(Transform mob, Collision2D c)
    {
        return null;
    }

    public I_ActorState OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }
}
