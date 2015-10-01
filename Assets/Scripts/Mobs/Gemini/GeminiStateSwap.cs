using System;
using UnityEngine;

class GeminiStateSwap : I_MobState
{
    private GeminiStats stats;
    private float timer;

    void I_ActorState.OnEnter(Transform mob)
    {
        // Set the sprite

        stats = mob.GetComponent<MobStats>() as GeminiStats;

        timer = this.stats.switchTime;
    }

    void I_ActorState.OnExit(Transform mob)
    {
        
    }

    I_ActorState I_ActorState.Update(Transform mob, float dt)
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
