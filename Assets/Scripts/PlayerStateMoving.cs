using UnityEngine;
using System.Collections;

public class PlayerStateMoving : I_PlayerState {

	private float vel;
	private float maxVel;
	private float slowDown;

	private Vector2 up;
	private Vector2 down;
	private Vector2 left;
	private Vector2 right;

	void I_PlayerState.OnEnter(Transform player)
	{
		player.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[0];

		vel = 1.0f;
		maxVel = 4.0f;
		slowDown = 0.5f;

		up = new Vector2(0f, vel);
		down = new Vector2(0f, -vel);
		left = new Vector2(-vel, 0f);
		right = new Vector2(vel, 0);
	}
	void I_PlayerState.OnExit(Transform player)
	{

	}
	
	// Update is called once per frame
	I_PlayerState I_PlayerState.Update(Transform player, float dt)
	{
		Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();

		// Check max speed
		if (Mathf.Abs(playerRB.velocity.x) > maxVel ||
		    Mathf.Abs(playerRB.velocity.y) > maxVel)
		{
			playerRB.velocity = Vector3.Normalize(playerRB.velocity) * maxVel;
		}
		
		// Check if the player is no longer moving
		if (!Input.GetKey(KeyCode.W) &&
		    !Input.GetKey(KeyCode.A) &&
		    !Input.GetKey(KeyCode.S) &&
		    !Input.GetKey(KeyCode.D) &&
		    playerRB.velocity == Vector2.zero)
		{
			return new PlayerStateIdle();
		}
		else
		{
			return null;
		}
	}

	I_PlayerState I_PlayerState.HandleInput(Transform player)
	{
		Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();

		// Roll
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			return new PlayerStateRoll();
		}

		// Moving Up
		if (Input.GetKey(KeyCode.W) && playerRB.velocity.y < maxVel)
		{
			playerRB.velocity += up;
		}
		
		// Moving Down
		if (Input.GetKey(KeyCode.S) && playerRB.velocity.y > -maxVel)
		{
			playerRB.velocity += down;
		}
		
		// Moving Left
		if (Input.GetKey(KeyCode.A) && playerRB.velocity.x > -maxVel)
		{
			playerRB.velocity += left;
		}
		
		// Moving Right
		if (Input.GetKey(KeyCode.D) && playerRB.velocity.x < maxVel)
		{
			playerRB.velocity += right;
		}
		
		// Vertical Slowdown
		if (!Input.GetKey(KeyCode.W) &&
		    !Input.GetKey(KeyCode.S))
		{
			if (playerRB.velocity.y > 0f)
			{
				if (playerRB.velocity.y - slowDown < 0f)
				{
					playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
				}
				else
				{
					playerRB.velocity -= new Vector2(0f, slowDown);
				}
			}
			else if (playerRB.velocity.y < 0f)
			{
				if (playerRB.velocity.y + slowDown > 0f)
				{
					playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
				}
				else
				{
					playerRB.velocity += new Vector2(0f, slowDown);
				}
			}
		}
		
		// Horizontal Slowdown
		if (!Input.GetKey(KeyCode.A) &&
		    !Input.GetKey(KeyCode.D))
		{
			if (playerRB.velocity.x > 0f)
			{
				if (playerRB.velocity.x - slowDown < 0f)
				{
					playerRB.velocity = new Vector2(0f, playerRB.velocity.y);
				}
				else
				{
					playerRB.velocity -= new Vector2(slowDown, 0f);
				}
			}
			else if (playerRB.velocity.x < 0f)
			{
				if (playerRB.velocity.x + slowDown > 0f)
				{
					playerRB.velocity = new Vector2(0f, playerRB.velocity.y);
				}
				else
				{
					playerRB.velocity += new Vector2(slowDown, 0f);
				}
			}
		}

		return null;
	}

	I_PlayerState I_PlayerState.OnCollisionEnter(Transform player, Collision2D c)
	{
		return null;
	}
}
