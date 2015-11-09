using UnityEngine;
using System.Collections;

public class HeroStateMoving : I_HeroState {

	private float speed;
	private float maxSpeed;
	private float slowDown;

	private Vector2 up;
	private Vector2 down;
	private Vector2 left;
	private Vector2 right;

    private HeroStats stats;

	void I_ActorState.OnEnter(Transform hero)
	{
        stats = hero.GetComponent<HeroStats>();
		hero.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[0];

		speed = stats.Speed;
		maxSpeed = stats.MaxSpeed;
		slowDown = stats.SlowDown;

		up = new Vector2(0f, speed);
		down = new Vector2(0f, -speed);
		left = new Vector2(-speed, 0f);
		right = new Vector2(speed, 0);
	}
	void I_ActorState.OnExit(Transform hero)
	{

	}
	
	// Update is called once per frame
	I_ActorState I_ActorState.Update(Transform hero, float dt)
	{
		Rigidbody2D heroRB = hero.GetComponent<Rigidbody2D>();

        // Update the max incase of buffs
        maxSpeed = stats.MaxSpeed;

		// Check max speed
		if (Mathf.Abs(heroRB.velocity.x) > maxSpeed ||
		    Mathf.Abs(heroRB.velocity.y) > maxSpeed)
		{
			heroRB.velocity = Vector3.Normalize(heroRB.velocity) * maxSpeed;
		}
		
		// Check if the hero is no longer moving
		if (!Input.GetKey(KeyCode.W) &&
		    !Input.GetKey(KeyCode.A) &&
		    !Input.GetKey(KeyCode.S) &&
		    !Input.GetKey(KeyCode.D) &&
		    heroRB.velocity == Vector2.zero)
		{
			return new HeroStateIdle();
		}
		else
		{
			return null;
		}
	}

	I_ActorState I_ActorState.HandleInput(Transform hero)
	{
		Rigidbody2D heroRB = hero.GetComponent<Rigidbody2D>();

		// Roll
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			return new HeroStateDash();
		}

		// Moving Up
		if (Input.GetKey(KeyCode.W) && heroRB.velocity.y < maxSpeed)
		{
			heroRB.velocity += up;
		}
		
		// Moving Down
		if (Input.GetKey(KeyCode.S) && heroRB.velocity.y > -maxSpeed)
		{
			heroRB.velocity += down;
		}
		
		// Moving Left
		if (Input.GetKey(KeyCode.A) && heroRB.velocity.x > -maxSpeed)
		{
			heroRB.velocity += left;
		}
		
		// Moving Right
		if (Input.GetKey(KeyCode.D) && heroRB.velocity.x < maxSpeed)
		{
			heroRB.velocity += right;
		}
		
		// Vertical Slowdown
		if (!Input.GetKey(KeyCode.W) &&
		    !Input.GetKey(KeyCode.S))
		{
            if (heroRB.velocity.y / slowDown < 0.01f &&
                heroRB.velocity.y / slowDown > -0.01f)
            {
                heroRB.velocity = new Vector2(heroRB.velocity.x, 0f);
            }
            else
            {
                heroRB.velocity = new Vector2(heroRB.velocity.x, heroRB.velocity.y / slowDown);
            }
        }

        // Horizontal Slowdown
        if (!Input.GetKey(KeyCode.A) &&
		    !Input.GetKey(KeyCode.D))
		{
            if (heroRB.velocity.x / slowDown < 0.01f &&
                heroRB.velocity.x / slowDown > -0.01f)
            {
                heroRB.velocity = new Vector2(0f, heroRB.velocity.y);
            }
            else
            {
                heroRB.velocity = new Vector2(heroRB.velocity.x / slowDown, heroRB.velocity.y);
            }
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
