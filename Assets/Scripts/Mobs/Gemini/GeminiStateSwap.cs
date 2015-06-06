using System;
using UnityEngine;

class GeminiStateSwap : I_MobState
{
    private GeminiStats stats;
    private float timer;

    void I_MobState.OnEnter(Transform mob, MobStats stats)
    {
        // Set the sprite

        this.stats = stats as GeminiStats;

        timer = this.stats.switchTime;
    }

    void I_MobState.OnExit(Transform mob)
    {
        
    }

    I_MobState I_MobState.Update(Transform mob, float dt)
    {
        if (timer <= 0)
        {
            // CHANGE PLACES
            Vector3 temp = mob.position;
            mob.position = stats.Twin.position;
            stats.Twin.position = temp;

            return new GeminiStateAlert();
        }

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

    I_MobState I_MobState.OnCollisionStay(Transform mob, Collision2D c)
    {
        return null;
    }
}
