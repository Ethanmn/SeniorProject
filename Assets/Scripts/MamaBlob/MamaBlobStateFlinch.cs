﻿using UnityEngine;
using System.Collections;

public class MamaBlobStateFlinch : I_NPCFlinchState {

    float timer;
	Vector2 vel;

	public MamaBlobStateFlinch(Vector2 vel)
	{
		this.vel = vel;
	}

	void I_NPCState.OnEnter(Transform npc, MobStats stats)
	{
		npc.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/MamaBlobPH")[2];
		npc.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
        timer = 0.1f;
	}
	void I_NPCState.OnExit(Transform npc)
	{
		npc.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	I_NPCState I_NPCState.Update(Transform npc, float dt)
	{
		if (timer <= 0)
		{

			return new MamaBlobStateAlert();
		}

		timer -= Time.deltaTime;
		
		return null;
	}
    I_NPCState I_NPCState.FixedUpdate(Transform npc, float dt)
    {
        return null;
    }
    I_NPCState I_NPCState.HandleInput(Transform npc)
	{
		return null;
	}
	I_NPCState I_NPCState.OnCollisionEnter(Transform npc, Collision2D c)
	{
		return null;
	}

	void I_NPCFlinchState.SetVel(Vector2 vel)
	{
		this.vel = vel;
	}
}
