using UnityEngine;
using System.Collections;

public interface I_NPCState {

	void OnEnter(Transform npc);
	void OnExit(Transform npc);
	
	// Update is called once per frame
	I_NPCState Update(Transform npc, float dt);
	I_NPCState HandleInput(Transform npc);
	I_NPCState OnCollisionEnter(Transform npc, Collision2D c);
	I_NPCState OnTriggerStay(Transform npc, Collider2D c);
}
