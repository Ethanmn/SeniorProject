using UnityEngine;
using System.Collections;
using System;

public class BlobStateDeath : I_MobState
{
    private float timer;
    private int blinkCount;
    private bool blink;
    private BlobStats stats;

    void I_ActorState.OnEnter(Transform mob)
    {
        timer = mob.GetComponent<MobStats>().deathTimer;
        blinkCount = 0;
        blink = false;
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        stats = mob.GetComponent<BlobStats>();
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

            // Did an rune drop?
            runeDrop(mob);

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

    protected Rune runeDrop(Transform trn)
    {
        // What item did they get?
        Rune get = null;

        // Chance to drop item
        int chance = UnityEngine.Random.Range(1, 101);

        // Get item find from player
        int itemFind = 0;

        // Did the player make it?
        if (chance + itemFind > stats.RuneChance)
        {
            // Yes!
            // Get a rune
            get = (Rune)Activator.CreateInstance(typeof(DoubleRune), new object[] { });
            get.Drop(trn.position);
        }

        // Return no item
        return get;
    }
}
