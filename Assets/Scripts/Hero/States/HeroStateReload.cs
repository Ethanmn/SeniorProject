using UnityEngine;
using System.Collections;

public class HeroStateReload : I_HeroState {

	float timer;
	float reloadTime;

    private HeroStats stats;

	void I_HeroState.OnEnter(Transform hero)
	{
		hero.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[4];

        stats = hero.GetComponent<HeroStats>();

		reloadTime = 0.5f;

		timer = reloadTime;
		hero.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
	}

	void I_HeroState.OnExit(Transform hero)
	{

	}
	
	// Update is called once per frame
	I_HeroState I_HeroState.Update(Transform hero, float dt)
	{
		if (stats.Ammo == stats.MaxAmmo)
		{
			return new HeroStateIdle();
		}

		if (timer < 0f)
		{
			stats.Ammo++;
			timer = reloadTime;
		}
		else
		{
			timer -= dt;
		}
		return null;
	}

	I_HeroState I_HeroState.HandleInput(Transform hero)
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

	I_HeroState I_HeroState.OnCollisionEnter(Transform hero, Collision2D c)
	{
		return null;
	}
}
