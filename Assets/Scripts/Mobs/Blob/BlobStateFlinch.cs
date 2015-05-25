using UnityEngine;
using System.Collections;

public class BlobStateFlinch : I_MobFlinchState {

    float timer;
	Vector2 vel;

	public BlobStateFlinch(Vector2 vel)
	{
		this.vel = vel;
	}

	void I_MobState.OnEnter(Transform mob, MobStats stats)
	{
		mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BlobPH")[2];
		mob.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
        timer = 0.1f;
	}
	void I_MobState.OnExit(Transform mob)
	{
		mob.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	I_MobState I_MobState.Update(Transform mob, float dt)
	{
		if (timer <= 0)
		{
			return new BlobStateAlert();
		}

		timer -= Time.deltaTime;
		
		return null;
	}
    I_MobState I_MobState.FixedUpdate(Transform mob, float dt)
    {
        return null;
    }
    I_MobState I_MobState.HandleInput(Transform mob)
	{
		return null;
	}
	I_MobState I_MobState.OnCollisionEnter(Transform mob, Collision2D c)
	{
		return null;
	}

	void I_MobFlinchState.SetVel(Vector2 vel)
	{
		this.vel = vel;
	}
}
