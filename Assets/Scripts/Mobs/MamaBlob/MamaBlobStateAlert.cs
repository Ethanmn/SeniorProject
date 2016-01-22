using UnityEngine;
using System.Collections;

public class MamaBlobStateAlert : I_MobState
{

    // The player the mob has been alerted to
    private Transform hero;
    // The mob's status script
    private MobStats stats;

    void I_ActorState.OnEnter(Transform mob)
    {
        mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/Mobs/MamaBlobPH")[1];

        hero = GameObject.FindGameObjectWithTag("Hero").gameObject.GetComponent<Transform>();

        stats = mob.GetComponent<MobStats>();
    }
    void I_ActorState.OnExit(Transform mob)
    {
        mob.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Update is called once per frame
    I_ActorState I_ActorState.Update(Transform mob, float dt)
    {
        Vector2 dir = hero.position - mob.position;
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
