using UnityEngine;
using System.Collections;

public class PlayerStateRoll : I_PlayerState {

	private float timer;
    private PlayerStats stats;


	void I_PlayerState.OnEnter(Transform player)
	{
		player.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[2];
        stats = player.GetComponent<PlayerStats>();

		timer = stats.DashTimer;

        Rigidbody2D playerRB;
        playerRB = player.GetComponent<Rigidbody2D>();

		playerRB.velocity = Vector3.Normalize(playerRB.velocity) * stats.Dash;

		// Reload one for rolling
		if (stats.Ammo < stats.MaxAmmo)
        {
            stats.Ammo++;
        }
	}

	void I_PlayerState.OnExit(Transform player)
	{

	}
	
	// Update is called once per frame
	I_PlayerState I_PlayerState.Update(Transform player, float dt)
	{
		if (timer <= 0f)
		{
			return new PlayerStateRollRecovery();
		}
		else
		{
			timer -= dt;
		}
		return null;
	}

	I_PlayerState I_PlayerState.HandleInput(Transform player)
	{
		return null;
	}

	I_PlayerState I_PlayerState.OnCollisionEnter(Transform player, Collision2D c)
	{
		return null;
	}
}
