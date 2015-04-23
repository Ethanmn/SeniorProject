using UnityEngine;
using System.Collections;

public interface PMoveState 
{
	void OnEnter(PlayerController pc);
	void OnExit(PlayerController pc);

	// Use this for initialization
	void Start (PlayerController pc);
	
	// Update is called once per frame
	void Update (PlayerController pc, float dt);

}

// Standing State
public class PMStandingState : PMoveState
{
	void PMoveState.OnEnter(PlayerController pc)
	{

	}
	void PMoveState.OnExit(PlayerController pc)
	{

	}
	public virtual void Start(PlayerController pc)
	{

	}

	public virtual void Update(PlayerController pc, float dt)
	{
		HandleInput(pc);
	}

	private void HandleInput(PlayerController pc)
	{
		if (Input.GetKey(KeyCode.W) ||
		    Input.GetKey(KeyCode.A) ||
		    Input.GetKey(KeyCode.S) ||
		    Input.GetKey(KeyCode.D)  )
		{
			pc.ChangeMoveState(new PMMovingState());
		}
	}
}

// Moving State
public class PMMovingState : PMoveState
{
	private float vel;
	private float maxVel;
	private float slowDown;

	private Vector2 up;
	private Vector2 down;
	private Vector2 left;
	private Vector2 right;

	void PMoveState.OnEnter(PlayerController pc)
	{
		vel = 1.0f;
		maxVel = 8.0f;
		slowDown = 0.5f;
		up = new Vector2(0f, vel);
		down = new Vector2(0f, -vel);
		left = new Vector2(-vel, 0f);
		right = new Vector2(vel, 0);
	}
	void PMoveState.OnExit(PlayerController pc)
	{

	}

	public virtual void Start(PlayerController pc)
	{

	}
	
	public virtual void Update(PlayerController pc, float dt)
	{
		Rigidbody2D playerRB = pc.gameObject.GetComponent<Rigidbody2D>();
		HandleInput(playerRB);

		HandleState(playerRB, pc);
	}

	public void SetSlowDown(float set)
	{
		slowDown = set;
	}

	private void HandleInput(Rigidbody2D playerRB)
	{
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
			//playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
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
			//playerRB.velocity = new Vector2(0f, playerRB.velocity.y);
		}
	}

	private void HandleState(Rigidbody2D playerRB, PlayerController pc)
	{
		if (!Input.GetKey(KeyCode.W) &&
		    !Input.GetKey(KeyCode.A) &&
		    !Input.GetKey(KeyCode.S) &&
		    !Input.GetKey(KeyCode.D) &&
		    playerRB.velocity == Vector2.zero)
		{
			pc.ChangeMoveState(new PMStandingState());
		}
	}
}