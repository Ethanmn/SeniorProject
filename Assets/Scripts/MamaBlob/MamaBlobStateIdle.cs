﻿using UnityEngine;
using System.Collections;

public class MamaBlobStateIdle : I_MobState {

	private int damage = 1;
	MobStats stats;

	void I_MobState.OnEnter(Transform mob, MobStats stats)
	{
		mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/MamaBlobPH")[0];
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

		if (Vector2.Distance(mob.position, player.position) <= stats.aggroRange)
		{
			Debug.Log("RAWR!");
			return new MamaBlobStateAlert();
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
		if (c.gameObject.CompareTag("Player"))
		{
			c.gameObject.GetComponent<PlayerController>().Hit(damage, mob);
		}

		return null;
	}
}