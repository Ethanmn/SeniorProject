using UnityEngine;
using System.Collections;

public class BlobStateAlert : I_NPCState {

	float speed;

	void I_NPCState.OnEnter(Transform npc)
	{
		npc.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BlobPH")[1];
		speed = 2.0f;
	}
	void I_NPCState.OnExit(Transform npc)
	{
		
	}
	
	// Update is called once per frame
	I_NPCState I_NPCState.Update(Transform npc, float dt)
	{
		Vector2 dir = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>().position - npc.position;
		Vector2 vel = dir.normalized * speed;
		npc.GetComponent<Rigidbody2D>().velocity = vel;

		return null;
	}
	I_NPCState I_NPCState.HandleInput(Transform npc)
	{
		return null;
	}
	I_NPCState I_NPCState.OnCollisionEnter(Transform npc, Collision2D c)
	{
		return null;
	}
	I_NPCState I_NPCState.OnTriggerEnter(Transform npc, Collider2D c)
	{
		return null;
	}
}
