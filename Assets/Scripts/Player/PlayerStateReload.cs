using UnityEngine;
using System.Collections;

public class PlayerStateReload : I_PlayerState {

	float timer;
	float reloadTime;

    private PlayerStats stats;

	void I_PlayerState.OnEnter(Transform player)
	{
		player.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[4];

        stats = player.GetComponent<PlayerStats>();

		reloadTime = 0.5f;

		timer = reloadTime;
		player.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
	}

	void I_PlayerState.OnExit(Transform player)
	{

	}
	
	// Update is called once per frame
	I_PlayerState I_PlayerState.Update(Transform player, float dt)
	{
		if (stats.Ammo == stats.MaxAmmo)
		{
			return new PlayerStateIdle();
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

	I_PlayerState I_PlayerState.HandleInput(Transform player)
	{
		if (Input.GetKey(KeyCode.W) ||
		    Input.GetKey(KeyCode.A) ||
		    Input.GetKey(KeyCode.S) ||
		    Input.GetKey(KeyCode.D))
		{
			return new PlayerStateMoving();
		}
		if (Input.GetMouseButtonUp(0))
		{
			return new PlayerStateIdle();
		}
		return null;
	}

	I_PlayerState I_PlayerState.OnCollisionEnter(Transform player, Collision2D c)
	{
		return null;
	}
}
