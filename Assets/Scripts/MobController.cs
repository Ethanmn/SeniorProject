﻿using UnityEngine;
using System.Collections;

public class MobController : MonoBehaviour{
	protected I_NPCState startState;
	protected I_NPCFlinchState flinchState;

	protected I_NPCState state;
	protected MobStats stats;

	// Use this for initialization
	void Start () {
		stats = gameObject.GetComponent<MobStats>();
		
		state = startState;
		state.OnEnter(transform, stats);
	}

	// Update is called once per frame
	void Update ()
	{
		I_NPCState newState = state.HandleInput(transform);
		if(newState != null)
		{
			SwitchState(newState);
		}
		
		newState = state.Update(transform, Time.deltaTime);
		
		if(newState != null)
		{
			SwitchState(newState);
		}
	}

	void FixedUpdate ()
	{
		if (stats.Dead)
		{
			Debug.Log(gameObject.name + " down!");
			GameObject.Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D c)
	{
		I_NPCState newState = state.OnCollisionEnter(transform, c);
		if(newState != null)
		{
			SwitchState(newState);
		}
	}

	// Return the current mob state
	public I_NPCState getState()
	{
		return this.state;
	}

	// Switch to a new state
	private void SwitchState(I_NPCState newState)
	{
		state.OnExit(transform);
		state = newState;
		state.OnEnter(transform, stats);
	}

	// Set the current mob state
	public void SetState(I_NPCState newState)
	{
		SwitchState(newState);
	}

	public void Hit(int damage, Vector2 vel)
	{
		if (!state.Equals(typeof(BlobStateFlinch)))
		{
			Debug.Log(gameObject.name + " Ouch!");

			flinchState.SetVel(vel);
			SetState(flinchState);
			
			// Take the damage
			stats.Health -= damage;
		}
	}
}
