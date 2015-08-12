using UnityEngine;

public interface I_HeroState {

	void OnEnter(Transform hero);
	void OnExit(Transform hero);
	
	// Update is called once per frame
	I_HeroState Update(Transform hero, float dt);
	I_HeroState HandleInput(Transform hero);
	I_HeroState OnCollisionEnter(Transform hero, Collision2D c);
	
}
