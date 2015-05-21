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
        Transform twin = null;
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        foreach (GameObject m in mobs)
        {
            GeminiStats mobStats = m.gameObject.GetComponent<MobStats>() as GeminiStats;
            if (mobStats != null && mobStats.gemID == stats.gemID)
            {
                twin = mobStats.gameObject.GetComponent<Transform>();
            }
        }

        float twinDist = -1;
        if (twin)
        {
             twinDist = Vector2.Distance(twin.position, mob.position);
        }

        Vector2 dir = player.position - mob.position;
        Vector2 vel = dir.normalized * stats.Speed;

        if (twinDist > stats.gemRange &&
            twinDist > 0)
        {
            // Retreat to twin
        }
        // Move towards player
        else
        {
            mob.GetComponent<Rigidbody2D>().velocity = vel;
        }
        
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
