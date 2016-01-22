using UnityEngine;
using System.Collections;

public class BlobStateIdle : I_MobState {

	MobStats stats;

	void I_ActorState.OnEnter(Transform mob)
	{
		mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/Mobs/BlobPH")[0];
		mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.stats = mob.GetComponent<MobStats>();
	}
	void I_ActorState.OnExit(Transform mob)
	{

	}
	
	// Update is called once per frame
	I_ActorState I_ActorState.Update(Transform mob, float dt)
	{
		GameObject hero = GameObject.FindGameObjectWithTag("Hero");

		mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Aggo to the player if they are in range
		if (hero != null && Vector2.Distance(mob.position, hero.transform.position) <= stats.aggroRange)
		{
			return new BlobStateAlert();
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
			c.gameObject.GetComponent<HeroController>().Hit(stats.damage, mob, Vector2.zero);
		}

		return null;
	}

    public I_ActorState OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }
}
