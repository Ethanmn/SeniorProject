using UnityEngine;
using System.Collections;

public class BlobStateFlinch : I_MobFlinchState {

    float timer;
	Vector2 vel;

	public BlobStateFlinch(Vector2 vel)
	{
		this.vel = vel;
	}

	void I_ActorState.OnEnter(Transform mob)
	{
		mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BlobPH")[2];
		mob.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
        timer = mob.GetComponent<MobStats>().flinchTimer;
	}
	void I_ActorState.OnExit(Transform mob)
	{
		//mob.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	I_ActorState I_ActorState.Update(Transform mob, float dt)
	{
        MobStats stats = mob.GetComponent<MobStats>();
        if (stats == null)
        {
            Debug.Log("STATS ERROR");
        }
		if (timer <= 0)
		{
            // It died
            if (stats.Health <= 0)
            {
                Debug.Log(mob.gameObject.name + " down!");
                //SwitchState(deathState);
                return new BlobStateDeath();
            }

            return new BlobStateAlert();
		}

		timer -= Time.deltaTime;
		
		return null;
	}
    I_MobState I_MobState.FixedUpdate(Transform mob, float dt)
    {
        return null;
    }
    I_ActorState I_ActorState.HandleInput(Transform mob)
	{
		return null;
	}
	I_ActorState I_ActorState.OnCollisionStay(Transform mob, Collision2D c)
	{
		return null;
	}

	void I_MobFlinchState.SetVel(Vector2 vel)
	{
		this.vel = vel;
	}

    public I_ActorState OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }
}
