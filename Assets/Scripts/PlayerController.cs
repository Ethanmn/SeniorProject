/* Credit to Kyle Piddington for the base */

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private int health = 3;
	private bool dead = false;

	private I_PlayerState state;

	public I_PlayerState getState()
	{
		return this.state;
	}

	// Use this for initialization
	void Start () {
		state = new PlayerStateIdle();
		state.OnEnter(transform);
	}
	
	// Update is called once per frame
	void Update ()
	{
		I_PlayerState newState = state.HandleInput(transform);
		if(newState != null)
		{
			SwitchState(newState);
		}

		newState = state.Update(transform, Time.deltaTime);

		if(newState != null)
		{
			SwitchState(newState);
		}
	}
	void FixedUpdate()
	{
		// Check if the game is over
		if (dead)
		{
			//SwitchState(PlayerStateDead);
		}
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		I_PlayerState newState = state.OnCollisionEnter(transform, c);
		if(newState != null)
		{
			SwitchState(newState);
		}
	}

	// Switch to a new state
	private void SwitchState(I_PlayerState newState)
	{
		state.OnExit(transform);
		state = newState;
		state.OnEnter(transform);
	}

	private void Damage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			dead = true;
		}
	}

	public void SetState(I_PlayerState newState)
	{
		SwitchState(newState);
	}

	public void Hit(int damage, Transform enemy)
	{
		Damage(damage);

		//SwitchState(new PlayerStateHurt(enemy));
	}
}
