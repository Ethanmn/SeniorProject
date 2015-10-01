using UnityEngine;
using System.Collections;
using System;

public class HeroStateIdle : I_HeroState {

	void I_ActorState.OnEnter(Transform hero)
	{
		hero.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[1];
	}

	void I_ActorState.OnExit(Transform hero)
	{

	}

    // Update is called once per frame
    I_ActorState I_ActorState.Update(Transform hero, float dt)
    {
		return null;
	}

	I_ActorState I_ActorState.HandleInput(Transform hero)
	{
		if (Input.GetKey(KeyCode.W) ||
		    Input.GetKey(KeyCode.A) ||
		    Input.GetKey(KeyCode.S) ||
		    Input.GetKey(KeyCode.D))
		{
			return new HeroStateMoving();
		}
		else
		{
			return null;
		}
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
