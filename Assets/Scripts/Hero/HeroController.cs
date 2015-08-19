﻿/* Credit to Kyle Piddington for the base */

using System;
using UnityEngine;

public class HeroController : MonoBehaviour {

    private HeroStats stats;

	private I_HeroState state;

	public I_HeroState GetState()
	{
		return this.state;
	}

	// Use this for initialization
	void Start () {
        stats = gameObject.GetComponent<HeroStats>();

		state = new HeroStateIdle();
		state.OnEnter(transform);
	}
	
	// Update is called once per frame
	void Update ()
	{
		I_HeroState newState = state.HandleInput(transform);
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
	void FixedUpdate()
	{
		// Check if the game is over
		if (stats.Dead)
		{
			SwitchState(new HeroStateDeath());
		}
	}

	void OnCollisionEnter2D(Collision2D c)
	{
        if (c.gameObject.CompareTag("Mob"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if (c.gameObject.CompareTag("Wall"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

		I_HeroState newState = state.OnCollisionEnter(transform, c);
		if(newState != null)
		{
			SwitchState(newState);
		}
	}

	// Switch to a new state
	private void SwitchState(I_HeroState newState)
	{
		state.OnExit(transform);
		state = newState;
		state.OnEnter(transform);
	}

	public void SetState(I_HeroState newState)
	{
		SwitchState(newState);
	}

    // Method called when the hero takes damage
	public void Hit(int damage, Transform enemy)
	{
        if (!stats.Flinching)
        {
            // Calculate how much damage is blocked by bonus defense
            int realDamage = damage - stats.BonusDefense;

            // Subtract damage from bonus defense
            stats.BonusDefense = Math.Max(0, stats.BonusDefense - damage);

            // Deal the damage
            // IF the "undershirt effect" (given by Runic Shield) is active, the hero has greater than one health, and the damage would be lethal
            if (stats.Undershirt && stats.Health > 1 && stats.Health - realDamage <= 0)
            {
                // Instead set health to one
                stats.Health = 1;
            }
            // ELSE
            else
            {
                // Deal the damage
                stats.Health -= Math.Max(0, realDamage);
            }

            // Make the hero flinch
            stats.Flinching = true;
            SwitchState(new HeroStateFlinch(enemy));

            // Raise the event for being hurt
            PublisherBox.onHurtPub.RaiseEvent(transform, enemy);
        }
	}
}