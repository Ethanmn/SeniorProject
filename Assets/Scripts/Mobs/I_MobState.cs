using UnityEngine;
using System.Collections;

public interface I_MobState {

	void OnEnter(Transform mob, MobStats stats);
	void OnExit(Transform mob);
	
	// Update is called once per frame
	I_MobState Update(Transform mob, float dt);
    I_MobState FixedUpdate(Transform mob, float dt);
    I_MobState HandleInput(Transform mob);
	I_MobState OnCollisionStay(Transform mob, Collision2D c);
}
