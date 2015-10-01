using UnityEngine;

public class GeminiStateIdle : I_MobState
{
    GeminiStats stats;

    void I_ActorState.OnEnter(Transform mob)
    {
        mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/GeminiPH")[0];
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        stats = mob.GetComponent<MobStats>() as GeminiStats;
    }

    void I_ActorState.OnExit(Transform mob)
    {
        
    }

    I_ActorState I_ActorState.Update(Transform mob, float dt)
    {
        Transform hero = GameObject.FindGameObjectWithTag("Hero").transform;

        // Don't move in idle state!
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Aggo to the player if they are in range
        if (Vector2.Distance(mob.position, hero.position) <= stats.aggroRange)
        {
            return new GeminiStateAlert();
        }
        else
            Debug.Log("Not in range!");
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
        // IF hit by a player, aggo to them
        return null;
    }

    public I_ActorState OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }
}
