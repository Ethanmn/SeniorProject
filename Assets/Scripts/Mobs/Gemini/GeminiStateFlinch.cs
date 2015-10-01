using UnityEngine;
using System.Collections;

public class GeminiStateFlinch : I_MobFlinchState
{
    float timer;
    Vector2 vel;
    GeminiStats stats;

    public GeminiStateFlinch(Vector2 vel)
    {
        this.vel = vel;
    }

    void I_ActorState.OnEnter(Transform mob)
    {
        mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/GeminiPH")[2];
        mob.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
        timer = 0.1f;

        stats = mob.GetComponent<MobStats>() as GeminiStats;
    }
    void I_ActorState.OnExit(Transform mob)
    {
        mob.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Update is called once per frame
    I_ActorState I_ActorState.Update(Transform mob, float dt)
    {
        if (timer <= 0)
        {
            // IF Gemini is low on heath AND Twin has HIGHER health, switch places
            if (stats.Health == 1 && stats.Twin &&
                stats.Twin.GetComponent<GeminiStats>().Health > 1)
            {
                return new GeminiStateSwap();
            }
            // ELSE do your usual thang
            return new GeminiStateAlert();
        }

        timer -= Time.deltaTime;

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

    void I_MobFlinchState.SetVel(Vector2 vel)
    {
        this.vel = vel;
    }

    public I_ActorState OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }
}
