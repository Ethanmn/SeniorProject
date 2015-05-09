using UnityEngine;
using System.Collections;

public class BlobStateFlinch : I_NPCState {

	float timer = 0.1f;
	Vector2 vel;

	public BlobStateFlinch(Vector2 vel)
	{
		this.vel = vel / 5f;
	}

	void I_NPCState.OnEnter(Transform npc)
	{
		npc.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BlobPH")[2];
		npc.gameObject.GetComponent<Rigidbody2D>().velocity = vel;
	}
	void I_NPCState.OnExit(Transform npc)
	{
		npc.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	I_NPCState I_NPCState.Update(Transform npc, float dt)
	{
		npc.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		if (timer <= 0)
		{

			return new BlobStateIdle();
		}

		timer -= Time.deltaTime;
		
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
