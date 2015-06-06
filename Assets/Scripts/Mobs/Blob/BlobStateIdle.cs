using UnityEngine;
using System.Collections;

public class BlobStateIdle : I_MobState {

	MobStats stats;

	void I_MobState.OnEnter(Transform mob, MobStats stats)
	{
		mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BlobPH")[0];
		mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.stats = stats;
	}
	void I_MobState.OnExit(Transform mob)
	{

	}
	
	// Update is called once per frame
	I_MobState I_MobState.Update(Transform mob, float dt)
	{
		Transform player = GameObject.FindGameObjectWithTag("Player").transform;

		mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Aggo to the player if they are in range
		if (Vector2.Distance(mob.position, player.position) <= stats.aggroRange)
		{
			return new BlobStateAlert();
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

	I_MobState I_MobState.OnCollisionStay(Transform mob, Collision2D c)
	{
        // IF hit by a player, aggo to them


        if (c.gameObject.CompareTag("Player"))
		{
			c.gameObject.GetComponent<PlayerController>().Hit(stats.damage, mob);
		}

		return null;
	}
}
