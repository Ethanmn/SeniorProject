using System;
using UnityEngine;
class GeminiStateAlert : I_MobState
{
    // The player the mob has been alerted to
    private Transform hero;
    // The mob's status script
    private GeminiStats stats;

    void I_ActorState.OnEnter(Transform mob)
    {
        mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/GeminiPH")[1];

        hero = GameObject.FindGameObjectWithTag("Hero").gameObject.GetComponent<Transform>();

        stats = mob.GetComponent<MobStats>() as GeminiStats;
    }

    void I_ActorState.OnExit(Transform mob)
    {
        
    }

    I_ActorState I_ActorState.Update(Transform mob, float dt)
    {
        float twinDist = -1;
        float mobDist = -1;
        // Make sure that they have a twin and that they can find the player before assigning movement
        if (stats.Twin && hero)
        {
            twinDist = Vector2.Distance(stats.Twin.position, hero.position);
            mobDist = Vector2.Distance(mob.position, hero.position);
        }

        Vector2 dir;
        // IF Twin is closer than this mob to the player, and alive, backout and let Twin fight it
        if (twinDist < mobDist && !stats.Twin.GetComponent<MobStats>().dead)
        {
            // Vector from player to Twin, normalized
            Vector3 back = (stats.Twin.position - hero.position).normalized;
            // Multiply by a distance
            back *= stats.gemRange;
            // Add to Twin's position
            Vector3 target = stats.Twin.position + back;
            if (Vector3.Distance(target, mob.position) < 0.1f)
            {
                dir = Vector2.zero;
            }
            else
            {
                // dir = calculated position - mob.position;
                dir = target - mob.position;
            }
                
        }
        // Move towards player
        else
        {
            dir = hero.position - mob.position;
        }
        Vector2 vel = dir.normalized * stats.Speed;
        mob.GetComponent<Rigidbody2D>().velocity = vel;

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
        if (c.gameObject.CompareTag("Hero"))
        {
            c.gameObject.GetComponent<HeroController>().Hit(stats.Damage, mob, Vector2.zero);
        }
        return null;
    }

    public I_ActorState OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }
}
