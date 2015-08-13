using UnityEngine;
using System.Collections;

public class HeroStateDashRecovery : I_HeroState {

	private float vel;
	private float maxVel;
	private float slowDown;
    private HeroStats stats;
	
	private Vector2 up;
	private Vector2 down;
	private Vector2 left;
	private Vector2 right;

	private float timer;
	
	void I_HeroState.OnEnter(Transform hero)
	{
		hero.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[3];
        stats = hero.GetComponent<HeroStats>();

		timer = 0;

		vel = 0.5f;
		maxVel = 2.0f;
		slowDown = 0.5f;

		up = new Vector2(0f, vel);
		down = new Vector2(0f, -vel);
		left = new Vector2(-vel, 0f);
		right = new Vector2(vel, 0);
	}
	void I_HeroState.OnExit(Transform hero)
	{
		
	}
	
	// Update is called once per frame
	I_HeroState I_HeroState.Update(Transform hero, float dt)
	{
		Rigidbody2D heroRB = hero.GetComponent<Rigidbody2D>();

		if (timer > stats.TiredTimer)
		{
            return new HeroStateMoving();
        }
		else
		{
            timer += dt;
		}

		// Check max speed
		if (Mathf.Abs(heroRB.velocity.x) > maxVel ||
		    Mathf.Abs(heroRB.velocity.y) > maxVel)
		{
			heroRB.velocity = Vector3.Normalize(heroRB.velocity) * maxVel;
		}

		return null;
	}
	
	I_HeroState I_HeroState.HandleInput(Transform hero)
	{
		Rigidbody2D heroRB = hero.GetComponent<Rigidbody2D>();
		
		// Moving Up
		if (Input.GetKey(KeyCode.W) && heroRB.velocity.y < maxVel)
		{
			heroRB.velocity += up;
		}
		
		// Moving Down
		if (Input.GetKey(KeyCode.S) && heroRB.velocity.y > -maxVel)
		{
			heroRB.velocity += down;
		}
		
		// Moving Left
		if (Input.GetKey(KeyCode.A) && heroRB.velocity.x > -maxVel)
		{
			heroRB.velocity += left;
		}
		
		// Moving Right
		if (Input.GetKey(KeyCode.D) && heroRB.velocity.x < maxVel)
		{
			heroRB.velocity += right;
		}
		
		// Vertical Slowdown
		if (!Input.GetKey(KeyCode.W) &&
		    !Input.GetKey(KeyCode.S))
		{
			if (heroRB.velocity.y > 0f)
			{
				if (heroRB.velocity.y - slowDown < 0f)
				{
					heroRB.velocity = new Vector2(heroRB.velocity.x, 0f);
				}
				else
				{
					heroRB.velocity -= new Vector2(0f, slowDown);
				}
			}
			else if (heroRB.velocity.y < 0f)
			{
				if (heroRB.velocity.y + slowDown > 0f)
				{
					heroRB.velocity = new Vector2(heroRB.velocity.x, 0f);
				}
				else
				{
					heroRB.velocity += new Vector2(0f, slowDown);
				}
			}
		}
		
		// Horizontal Slowdown
		if (!Input.GetKey(KeyCode.A) &&
		    !Input.GetKey(KeyCode.D))
		{
			if (heroRB.velocity.x > 0f)
			{
				if (heroRB.velocity.x - slowDown < 0f)
				{
					heroRB.velocity = new Vector2(0f, heroRB.velocity.y);
				}
				else
				{
					heroRB.velocity -= new Vector2(slowDown, 0f);
				}
			}
			else if (heroRB.velocity.x < 0f)
			{
				if (heroRB.velocity.x + slowDown > 0f)
				{
					heroRB.velocity = new Vector2(0f, heroRB.velocity.y);
				}
				else
				{
					heroRB.velocity += new Vector2(slowDown, 0f);
				}
			}
		}
		
		return null;
	}
	
	I_HeroState I_HeroState.OnCollisionEnter(Transform hero, Collision2D c)
	{
		return null;
	}
}
