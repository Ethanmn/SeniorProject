using UnityEngine;
using System.Collections;

public class PlayerStateRoll : I_PlayerState {

	float timer;

	void I_PlayerState.OnEnter(Transform player)
	{
		player.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[2];

		Rigidbody2D playerRB;

		float dash = 10.0f;
		timer = 0.15f;

		playerRB = player.GetComponent<Rigidbody2D>();

		playerRB.velocity = Vector3.Normalize(playerRB.velocity) * dash;
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
