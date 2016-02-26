using UnityEngine;
using System.Collections;
using System;

public class GeminiStateDeath : I_MobState
{
    private float timer;
    private int blinkCount;
    private bool blink;

    private GeminiStats stats;
    private Color mobC;

    void I_ActorState.OnEnter(Transform mob)
    {
        mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/Mobs/GeminiPH")[3];
        
        blinkCount = 0;
        blink = false;
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        stats = mob.GetComponent<MobStats>() as GeminiStats;
        mobC = mob.GetComponent<SpriteRenderer>().color;
        timer = stats.deathTimer;
    }

    void I_ActorState.OnExit(Transform mob)
    {
        timer = stats.deathTimer;
        mob.GetComponent<SpriteRenderer>().color = new Color(mobC.r, mobC.g, mobC.b, 1f);
    }

    I_ActorState I_ActorState.Update(Transform mob, float dt)
    {
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (blink)
        {
            mob.GetComponent<SpriteRenderer>().color = new Color(mobC.r,mobC.g, mobC.b, 0);
        }
        else
        {
            mob.GetComponent<SpriteRenderer>().color = new Color(mobC.r, mobC.g, mobC.b, 1f);
        }

        if (timer <= 0)
        {
            // IF the Twin is ALSO dead, then die
            if (!stats.Twin || stats.Twin.GetComponent<GeminiStats>().dead || stats.Twin.GetComponent<GeminiStats>().incap)
            {
                stats.dead = true;

                runeDrop(mob);

                PublisherBox.onKillPub.RaiseEvent(mob);
                GameObject.Destroy(mob.gameObject);
            }
            else
            {
                // If they have not died by now, Come back to life
                Debug.Log("I'm not dead YET! " + stats.MaxHealth + stats.Dead);
                stats.Health = stats.MaxHealth;
                return new GeminiStateAlert();
            }
        }

        // IF the Twin is ALSO dead, then die
        if (!stats.Twin || stats.Twin.GetComponent<GeminiStats>().dead || stats.Twin.GetComponent<GeminiStats>().incap)
        {
            // Increase the speed of the death timer
            timer -= dt * 2f;
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
        if (chance + itemFind > 95)
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
