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

    private HeroInventory inventory;

	// Use this for initialization
	void Start () {
        stats = gameObject.GetComponent<HeroStats>();

		state = new HeroStateIdle();
		state.OnEnter(transform);

        inventory = gameObject.GetComponent<HeroInventory>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        // Update the state
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

        // Method to handle active items
        UpdateActiveItem();
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

    /// <summary>
    /// Sets the state of hero
    /// </summary>
    /// <param name="newState"></param>
	public void SetState(I_HeroState newState)
	{
		SwitchState(newState);
	}

    // Method called when the hero takes damage
	public void Hit(int damage, Transform enemy)
	{
        if (!stats.Flinching)
        {
            // Call damage function
            HitDamage(damage);

            // Make the hero flinch
            SwitchState(new HeroStateFlinch(enemy));

            // Raise the event for being hurt
            PublisherBox.onHurtPub.RaiseEvent(transform, enemy);
        }
	}

    public void HitNoFlinch(int damage, Transform enemy)
    {
        // Call damage function
        HitDamage(damage);

        // Raise the event for being hurt
        PublisherBox.onHurtPub.RaiseEvent(transform, enemy);
    }

    private void HitDamage(int damage)
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
            // Temp store the tempHealth
            int temp = stats.TempHealth;
            // Deal damage to the temp health
            stats.TempHealth -= Math.Min(realDamage, stats.TempHealth);
            realDamage -= temp - stats.TempHealth;
            // Deal the damage to real health
            stats.Health -= Math.Max(0, realDamage);
        }
    }

    public void Heal(int heal)
    {
        stats.Health += (int)(heal * stats.HealMultiplier);
    }

    // Checks for active item activations
    private void UpdateActiveItem()
    {
        if (Input.GetMouseButtonUp(1))
        {
            inventory.Active.UseActive();
        }
    }
}
