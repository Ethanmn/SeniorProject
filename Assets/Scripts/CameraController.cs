/* Credit to Kyle Piddington for the base */

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	private I_CameraState state;
	
	public I_CameraState getState()
	{
		return this.state;
	}
	
	// Use this for initialization
	void Start () {
		state = new CameraStatePlay();
		state.OnEnter(transform);
	}
	
	// Update is called once per frame
	void Update ()
	{
		I_CameraState newState = state.HandleInput(transform);
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
		I_CameraState newState = state.OnCollisionEnter(transform, c);
		if(newState != null)
		{
			SwitchState(newState);
		}
	}
	
	// Switch to a new state
	private void SwitchState(I_CameraState newState)
	{
		state.OnExit(transform);
		state = newState;
		state.OnEnter(transform);
	}
}
