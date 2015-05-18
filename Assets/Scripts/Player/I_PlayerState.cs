using UnityEngine;
using System.Collections;

public interface I_PlayerState {

	void OnEnter(Transform player);
	void OnExit(Transform player);
	
	// Update is called once per frame
	I_PlayerState Update(Transform player, float dt);
	I_PlayerState HandleInput(Transform player);
	I_PlayerState OnCollisionEnter(Transform player, Collision2D c);
	
}
