using UnityEngine;
using System.Collections;

public class MamaBlobStateIdle : I_MobState {

	private int damage = 1;
	MobStats stats;

	void I_ActorState.OnEnter(Transform mob)
	{
		mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/MamaBlobPH")[0];
		mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        stats = mob.GetComponent<MobStats>() as MamaBlobStats;
    }
	void I_ActorState.OnExit(Transform mob)
	{

	}
	
	// Update is called once per frame
	I_ActorState I_ActorState.Update(Transform mob, float dt)
	{
		Transform hero = GameObject.FindGameObjectWithTag("Hero").transform;

		mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

		if (Vector2.Distance(mob.position, hero.position) <= stats.aggroRange)
		{
			return new MamaBlobStateAlert();
		}

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

        if (c.gameObject.CompareTag("Hero"))
		{
			c.gameObject.GetComponent<HeroController>().Hit(damage, mob, Vector2.zero);
		}

		return null;
	}

    public I_ActorState OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }
}
