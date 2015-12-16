using UnityEngine;
using System.Collections;

public class MamaBlobStateFlinch : I_MobFlinchState {

    float timer;
	Vector2 vel;

	public MamaBlobStateFlinch(Vector2 vel)
	{
		this.vel = vel;
	}

	void I_ActorState.OnEnter(Transform mob)
	{
		mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/MamaBlobPH")[2];
		mob.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
        timer = 0.1f;
	}
	void I_ActorState.OnExit(Transform mob)
	{
		mob.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	I_ActorState I_ActorState.Update(Transform mob, float dt)
	{
		if (timer <= 0)
		{
            MobStats stats = mob.GetComponent<MamaBlobStats>();
            // It died
            if (stats.Health <= 0)
            {
                Debug.Log(mob.gameObject.name + " down!");
                //SwitchState(deathState);
                return new MamaBlobStateDeath();
            }
            return new MamaBlobStateAlert();
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
