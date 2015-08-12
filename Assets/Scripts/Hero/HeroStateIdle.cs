using UnityEngine;
using System.Collections;

public class HeroStateIdle : I_HeroState {

	void I_HeroState.OnEnter(Transform hero)
	{
		hero.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/PlayerPH")[1];
	}

	void I_HeroState.OnExit(Transform hero)
	{

	}

	// Update is called once per frame
	I_HeroState I_HeroState.Update (Transform hero, float dt)
	{
		return null;
	}

	I_HeroState I_HeroState.HandleInput(Transform hero)
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

	I_HeroState I_HeroState.OnCollisionEnter(Transform hero, Collision2D c)
	{
		return null;
	}
}
