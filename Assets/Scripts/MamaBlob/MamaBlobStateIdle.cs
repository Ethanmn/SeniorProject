using UnityEngine;
using System.Collections;

public class MamaBlobStateIdle : I_NPCState {

	private int damage = 1;
	MobStats stats;

	void I_NPCState.OnEnter(Transform npc, MobStats stats)
	{
		npc.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/MamaBlobPH")[0];
		npc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.stats = stats;
	}
	void I_NPCState.OnExit(Transform npc)
	{

	}
	
	// Update is called once per frame
	I_NPCState I_NPCState.Update(Transform npc, float dt)
	{
		Transform player = GameObject.FindGameObjectWithTag("Player").transform;

		npc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

		if (Vector2.Distance(npc.position, player.position) <= stats.aggroRange)
		{
			Debug.Log("RAWR!");
			return new MamaBlobStateAlert();
		}

		return null;
	}
    I_NPCState I_NPCState.FixedUpdate(Transform npc, float dt)
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
}
