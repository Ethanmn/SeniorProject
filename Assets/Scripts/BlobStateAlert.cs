using UnityEngine;
using System.Collections;

public class BlobStateAlert : I_NPCState {

	float speed;

	Transform player;

	void I_NPCState.OnEnter(Transform npc)
	{
		npc.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BlobPH")[1];

		player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();

		speed = 2.0f;
	}
	void I_NPCState.OnExit(Transform npc)
	{
		npc.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	I_NPCState I_NPCState.Update(Transform npc, float dt)
	{
		Vector2 dir = player.position - npc.position;
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
	I_NPCState I_NPCState.OnTriggerStay(Transform npc, Collider2D c)
	{
		return null;
	}
}
