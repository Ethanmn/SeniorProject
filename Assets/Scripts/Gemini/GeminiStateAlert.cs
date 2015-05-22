using System;
using UnityEngine;
class GeminiStateAlert : I_MobState
{
    // The player the mob has been alerted to
    private Transform player;
    // The mob's status script
    private GeminiStats stats;

    void I_MobState.OnEnter(Transform mob, MobStats stats)
    {
        mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/GeminiPH")[1];

        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();

        this.stats = stats as GeminiStats;
    }

    void I_MobState.OnExit(Transform mob)
    {
        
    }

    I_MobState I_MobState.Update(Transform mob, float dt)
    {
        float twinDist = -1;
        float mobDist = -1;
        if (stats.Twin)
        {
            twinDist = Vector2.Distance(stats.Twin.position, player.position);
            mobDist = Vector2.Distance(mob.position, player.position);
        }

        Vector2 dir;
        // IF Twin is closer than this mob to the player, and alive, backout and let Twin fight it
        if (twinDist < mobDist && !stats.Twin.GetComponent<MobStats>().dead)
        {
            // Vector from player to Twin, normalized
            Vector3 back = (stats.Twin.position - player.position).normalized;
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
            dir = player.position - mob.position;
        }
        Vector2 vel = dir.normalized * stats.Speed;
        mob.GetComponent<Rigidbody2D>().velocity = vel;

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
        if (c.gameObject.CompareTag("Player"))
        {
            c.gameObject.GetComponent<PlayerController>().Hit(stats.Damage, mob);
        }
        return null;
    }
}
