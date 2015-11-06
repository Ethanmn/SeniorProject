using UnityEngine;
using System.Collections;
using System;

public class HeroStateReload : I_HeroState {

    // Timer for how long it takes to reload
	private float timer;

    private HeroStats stats;

	void I_ActorState.OnEnter(Transform hero)
	{
		hero.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[4];

        stats = hero.GetComponent<HeroStats>();

		timer = stats.ReloadTime;
        hero.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
	}

	void I_ActorState.OnExit(Transform hero)
	{

	}
	
	// Update is called once per frame
	I_ActorState I_ActorState.Update(Transform hero, float dt)
	{
        // IF the weapon is done reloading
		if (stats.Ammo == stats.MaxAmmo)
		{
            // Return to an idle state
			return new HeroStateIdle();
		}
        // If the timer has run out
		if (timer < 0f)
		{
            // Increase the ammo
			stats.Ammo += stats.ReloadAmmo;
			timer = stats.ReloadTime;
		}
        // ELSE
		else
		{
            // Count down
			timer -= dt;
		}
		return null;
	}

	I_ActorState I_ActorState.HandleInput(Transform hero)
	{
		if (Input.GetKey(KeyCode.W) ||
		    Input.GetKey(KeyCode.A) ||
		    Input.GetKey(KeyCode.S) ||
		    Input.GetKey(KeyCode.D))
		{
			return new HeroStateMoving();
		}
		if (Input.GetMouseButtonUp(0))
		{
			return new HeroStateIdle();
		}
		return null;
	}

	I_ActorState I_ActorState.OnCollisionEnter(Transform hero, Collision2D c)
	{
		return null;
	}

    I_ActorState I_ActorState.OnCollisionStay(Transform actor, Collision2D c)
    {
        return null;
    }
}
