using UnityEngine;
using System.Collections;

public class BlobStateIdle : I_NPCState {

	private int damage = 1;

	void I_NPCState.OnEnter(Transform npc)
	{
		npc.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BlobPH")[0];
	}
	void I_NPCState.OnExit(Transform npc)
	{

	}
	
	// Update is called once per frame
	I_NPCState I_NPCState.Update(Transform npc, float dt)
	{
		return null;
	}
	I_NPCState I_NPCState.HandleInput(Transform npc)
	{
		return null;
	}
	I_NPCState I_NPCState.OnCollisionEnter(Transform npc, Collision2D c)
	{
		if (c.gameObject.CompareTag("Player"))
		{
			c.gameObject.GetComponent<PlayerController>().Hit(damage, npc);
		}

		return null;
	}
	I_NPCState I_NPCState.OnTriggerStay(Transform npc, Collider2D c)
	{
		if (c.gameObject.CompareTag("Player"))
		{
			return new BlobStateAlert();
		}

		return null;
	}
}
