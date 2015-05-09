using UnityEngine;
using System.Collections;

public class BlobController : MonoBehaviour {

	private I_NPCState state;
	private bool dead;
	private int health = 2;
	
	public I_NPCState getState()
	{
		return this.state;
	}
	
	// Use this for initialization
	void Start () {
		state = new BlobStateIdle();
		state.OnEnter(transform);
	}
	
	// Update is called once per frame
	void Update ()
	{
		I_NPCState newState = state.HandleInput(transform);
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

	void FixedUpdate ()
	{
		if (dead)
		{
			Debug.Log("Blobby down!");
			GameObject.Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D c)
	{
		I_NPCState newState = state.OnCollisionEnter(transform, c);
		if(newState != null)
		{
			SwitchState(newState);
		}
	}

	void OnTriggerStay2D(Collider2D c)
	{
		I_NPCState newState = state.OnTriggerStay(transform, c);
		if(newState != null)
		{
			SwitchState(newState);
		}
	}
	
	// Switch to a new state
	private void SwitchState(I_NPCState newState)
	{
		state.OnExit(transform);
		state = newState;
		state.OnEnter(transform);
	}
	
	public void SetState(I_NPCState newState)
	{
		SwitchState(newState);
	}

	public void Hit(int damage, Vector2 vel)
	{
		if (!state.Equals(typeof(BlobStateFlinch)))
		{
			Debug.Log("Ouch!");
			SetState(new BlobStateFlinch(vel));
			
			TakeDamage(damage);
		}
	}

	private void TakeDamage(int damage)
	{
		this.health -= damage;

		if (this.health <= 0)
			dead = true;
	}

}
