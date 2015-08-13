using UnityEngine;
using System.Collections;

public class HeroStateDash : I_HeroState {

	private float timer;
    private HeroStats stats;


	void I_HeroState.OnEnter(Transform hero)
	{
        // Set the sprite to dash sprite
		hero.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[2];
        stats = hero.GetComponent<HeroStats>();

        // Restart the timer
		timer = 0;

        // Assign the hero velocity
        Rigidbody2D heroRB;
        heroRB = hero.GetComponent<Rigidbody2D>();
		heroRB.velocity = Vector3.Normalize(heroRB.velocity) * stats.Dash;

		// Reload one for rolling
		if (stats.Ammo < stats.MaxAmmo)
        {
            stats.Ammo++;
        }

        // Tell publisher to signal a dash
        PublisherBox.onDashPub.RaiseEvent(hero);
	}

	void I_HeroState.OnExit(Transform hero)
	{

	}
	
	// Update is called once per frame
	I_HeroState I_HeroState.Update(Transform hero, float dt)
	{
		if (timer >= stats.DashTimer)
		{
			return new HeroStateDashRecovery();
		}
		else
		{
			timer += dt;
		}
		return null;
	}

	I_HeroState I_HeroState.HandleInput(Transform hero)
	{
		return null;
	}

	I_HeroState I_HeroState.OnCollisionEnter(Transform hero, Collision2D c)
	{
		return null;
	}
}
