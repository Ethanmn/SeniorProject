using UnityEngine;
using System.Collections;

public class MamaBlobStateAlert : I_MobState
{

    // The player the mob has been alerted to
    private Transform player;
    // The mob's status script
    private MobStats stats;

    void I_MobState.OnEnter(Transform mob, MobStats stats)
    {
        mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/MamaBlobPH")[1];

        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();

        this.stats = stats;
    }
    void I_MobState.OnExit(Transform mob)
    {
        mob.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Update is called once per frame
    I_MobState I_MobState.Update(Transform mob, float dt)
    {
        Vector2 dir = player.position - mob.position;
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
    I_MobState I_MobState.OnCollisionStay(Transform mob, Collision2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            c.gameObject.GetComponent<PlayerController>().Hit(stats.Damage, mob);
        }
        return null;
    }
}
