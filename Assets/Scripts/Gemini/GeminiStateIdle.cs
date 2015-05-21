using UnityEngine;

public class GeminiStateIdle : I_MobState
{
    GeminiStats stats;

    void I_MobState.OnEnter(Transform mob, MobStats stats)
    {
        mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/GeminiPH")[0];
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.stats = stats as GeminiStats;
    }

    void I_MobState.OnExit(Transform mob)
    {
        
    }

    I_MobState I_MobState.Update(Transform mob, float dt)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        // Don't move in idle state!
        mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Aggo to the player if they are in range
        if (Vector2.Distance(mob.position, player.position) <= stats.aggroRange)
        {
            return new GeminiStateAlert();
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
