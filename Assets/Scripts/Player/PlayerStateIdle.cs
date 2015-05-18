using UnityEngine;
using System.Collections;

public class PlayerStateIdle : I_PlayerState {

	void I_PlayerState.OnEnter(Transform player)
	{
		player.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[1];
	}

	void I_PlayerState.OnExit(Transform player)
	{

	}

	// Update is called once per frame
	I_PlayerState I_PlayerState.Update (Transform player, float dt)
	{
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
		else
		{
			return null;
		}
	}

	I_PlayerState I_PlayerState.OnCollisionEnter(Transform player, Collision2D c)
	{
		return null;
	}
}
