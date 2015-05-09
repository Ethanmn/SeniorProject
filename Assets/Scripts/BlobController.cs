using UnityEngine;
using System.Collections;

public class BlobController : MonoBehaviour {

	private I_NPCState state;
	
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
	
	void OnCollisionEnter2D(Collision2D c)
	{
		I_NPCState newState = state.OnCollisionEnter(transform, c);
		if(newState != null)
		{
			SwitchState(newState);
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		I_NPCState newState = state.OnTriggerEnter(transform, c);
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
}
