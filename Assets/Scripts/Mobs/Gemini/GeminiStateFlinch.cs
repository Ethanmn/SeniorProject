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

    void I_MobState.OnEnter(Transform mob, MobStats stats)
    {
        mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/GeminiPH")[2];
        mob.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
        timer = 0.1f;

        this.stats = stats as GeminiStats;
    }
    void I_MobState.OnExit(Transform mob)
    {
        mob.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Update is called once per frame
    I_MobState I_MobState.Update(Transform mob, float dt)
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
    I_MobState I_MobState.HandleInput(Transform mob)
    {
        return null;
    }
    I_MobState I_MobState.OnCollisionStay(Transform mob, Collision2D c)
    {
        return null;
    }

    void I_MobFlinchState.SetVel(Vector2 vel)
    {
        this.vel = vel;
    }
}
