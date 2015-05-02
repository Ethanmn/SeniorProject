using UnityEngine;
using System.Collections;

public class CameraStatePlay : I_CameraState {

	void I_CameraState.OnEnter(Transform camera)
	{

	}

	void I_CameraState.OnExit(Transform camera)
	{

	}
	
	// Update is called once per frame
	I_CameraState I_CameraState.Update(Transform camera, float dt)
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		camera.position = new Vector3(player.transform.position.x, 
		                              player.transform.position.y,
		                              -10f);
		return null;
	}

	I_CameraState I_CameraState.HandleInput(Transform camera)
	{
		return null;
	}

	I_CameraState I_CameraState.OnCollisionEnter(Transform camera, Collision2D c)
	{
		return null;
	}
}
