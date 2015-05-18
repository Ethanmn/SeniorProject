using UnityEngine;
using System.Collections;

public class PlayerStateRollRecovery : I_PlayerState {

	private float vel;
	private float maxVel;
	private float slowDown;
	
	private Vector2 up;
	private Vector2 down;
	private Vector2 left;
	private Vector2 right;

	private float timer;
	
	void I_PlayerState.OnEnter(Transform player)
	{
		player.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[3];

		timer = 0.7f;

		vel = 0.5f;
		maxVel = 2.0f;
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

		if (timer > 0)
		{
			timer -= dt;
		}
		else
		{
			return new PlayerStateMoving();
		}

		// Check max speed
		if (Mathf.Abs(playerRB.velocity.x) > maxVel ||
		    Mathf.Abs(playerRB.velocity.y) > maxVel)
		{
			playerRB.velocity = Vector3.Normalize(playerRB.velocity) * maxVel;
		}

		return null;
	}
	
	I_PlayerState I_PlayerState.HandleInput(Transform player)
	{
		Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
		
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
