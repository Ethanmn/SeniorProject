using UnityEngine;
using System.Collections;

public interface I_CameraState {

	void OnEnter(Transform camera);
	void OnExit(Transform camera);
	
	// Update is called once per frame
	I_CameraState Update(Transform camera, float dt);
	I_CameraState HandleInput(Transform camera);
	I_CameraState OnCollisionEnter(Transform camera, Collision2D c);
}
