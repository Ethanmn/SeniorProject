/* Credit to Kyle Piddington for the base */

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
            // Make the hero flinch
            SwitchState(new HeroStateFlinch(attacker));

            // Raise the event for being hurt
            PublisherBox.onHurtPub.RaiseEvent(transform, attacker);
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
                ChangeHealth(Math.Max(0, realDamage) * -1);
                //stats.Health -= Math.Max(0, realDamage);
            }
        }
    }

    public override void Heal(int heal)
    {
        stats.Health += (int)(heal * stats.HealMultiplier);
    }

    private void ChangeHealth(int change)
    {
        print("Change " + change);
        stats.Health += change;

        Canvas can = gameObject.GetComponentInChildren<Canvas>();
        GameObject text = Instantiate(Resources.Load("Prefabs/HealthChangeText")) as GameObject;
        text.GetComponent<Text>().text = "-1";//(change > 0 ? "+" : "") + change.ToString();
        text.transform.SetParent(can.transform);
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
