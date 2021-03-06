﻿/* Credit to Kyle Piddington for the base */

using System;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : ActorController {

    // Hero's stats
    private HeroStats stats;
    // Hero's invetory
    private HeroInventory inventory;

	// Use this for initialization
	public override void Start () {
        startState = new HeroStateIdle();
        base.Start();

        // Set up the stats
        stats = gameObject.GetComponent<HeroStats>();
        // Set up the inventory
        inventory = gameObject.GetComponent<HeroInventory>();
	}
	
	// Update is called once per frame
	public override void Update ()
	{
        base.Update();

        // Method to handle active items
        UpdateActiveItem();
	}
	public override void FixedUpdate()
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

		I_ActorState newState = state.OnCollisionEnter(transform, c);
		if(newState != null)
		{
			SwitchState(newState);
		}
	}

    // Method called when the hero takes damage
	public override void Hit(int damage, Transform attacker, Vector2 velocity)
	{
        base.Hit(damage, attacker, velocity);

        // IF the character is not already flinching
        if (!stats.Flinching)
        {
            // Raise the event for being hurt
            PublisherBox.onHurtPub.RaiseEvent(transform, attacker);

            // Make the hero flinch
            SwitchState(new HeroStateFlinch(attacker));
        }
	}

    public override void HitNoFlinch(int damage, Transform attacker)
    {
        base.HitNoFlinch(damage, attacker);

        if (!stats.Flinching)
        {
            // Raise the event for being hurt
            PublisherBox.onHurtPub.RaiseEvent(transform, attacker);
        }
    }

    protected override void HitDamage(int damage)
    {
        base.HitDamage(damage);

        if (!stats.Flinching)
        {
            // Calculate how much damage is blocked by bonus defense
            int realDamage = Math.Max(0, damage - stats.BonusDefense);

            // Subtract damage from bonus defense
            stats.BonusDefense = Math.Max(0, stats.BonusDefense - damage);

            // Deal the damage
            // Temp store the tempHealth
            int temp = stats.TempHealth;
            // Deal damage to the temp health
            stats.TempHealth -= Math.Min(realDamage, stats.TempHealth);
            realDamage -= temp - stats.TempHealth;
            // Deal the damage to real health
            ChangeHealth(realDamage * -1);
        }
    }

    public override void Heal(int heal)
    {
        if (stats == null)
        {
            print("ERROR!! No stats!");
        }
        ChangeHealth((int)(heal * stats.HealMultiplier));
        //stats.Health += (int)(heal * stats.HealMultiplier);
    }

    protected override void ChangeHealth(int change)
    {
        int healthChange;
        int oldHealth = stats.Health;

        stats.Health += change;
        healthChange = stats.Health - oldHealth;

        Canvas can = gameObject.GetComponentInChildren<Canvas>();
        GameObject text = Instantiate(Resources.Load("Prefabs/UIHealthChangeText")) as GameObject;
        text.GetComponent<Text>().text = (healthChange >= 0 ? "+" : "") + healthChange.ToString();
        text.transform.SetParent(can.transform, false);
    }

    // Checks for active item activations
    private void UpdateActiveItem()
    {
        if (Input.GetMouseButtonUp(1) && inventory.Active != null)
        {
            inventory.Active.UseActive();
        }
    }
}
