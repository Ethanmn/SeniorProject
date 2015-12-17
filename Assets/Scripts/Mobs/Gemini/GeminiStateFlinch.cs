using UnityEngine;
using System.Collections;

public class GeminiStateFlinch : I_MobFlinchState
{
    float timer;
    Vector2 vel;
    GeminiStats stats;
    Transform hero;

    public GeminiStateFlinch(Vector2 vel)
    {
        this.vel = vel;
    }

    void I_ActorState.OnEnter(Transform mob)
    {
        mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/GeminiPH")[2];
        mob.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
        timer = mob.GetComponent<MobStats>().flinchTimer;

        hero = GameObject.FindGameObjectWithTag("Hero").gameObject.GetComponent<Transform>();

        stats = mob.GetComponent<MobStats>() as GeminiStats;
    }
    void I_ActorState.OnExit(Transform mob)
    {
        //mob.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Update is called once per frame
    I_ActorState I_ActorState.Update(Transform mob, float dt)
    {
        if (timer <= 0)
        {
            float twinDist = -1;
            float mobDist = -1;
            // Make sure that they have a twin and that they can find the player before assigning movement
            if (stats.Twin && hero)
            {
                twinDist = Vector2.Distance(stats.Twin.position, hero.position);
                mobDist = Vector2.Distance(mob.position, hero.position);

                // IF Gemini is low on heath AND Twin has HIGHER health AND you are closer, switch places
                if (stats.Health == 1 && stats.Twin &&
                    stats.Twin.GetComponent<GeminiStats>().Health > 1 &&
                    twinDist > mobDist)
                {
                    return new GeminiStateSwap();
                }
            }
            
            // IF it died
            if (stats.Health <= 0)
            {
                Debug.Log(mob.gameObject.name + " down!");
                //SwitchState(deathState);
                return new GeminiStateDeath();
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
