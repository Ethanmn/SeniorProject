using UnityEngine;
using System.Collections;

public class BlobStateAlert : I_NPCState {

	// The player the mob has been alerted to
	private Transform player;
	// The mob's status script
	private MobStats stats;

    void I_NPCState.OnEnter(Transform npc, MobStats stats)
	{
		npc.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/BlobPH")[1];

		player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();

        this.stats = stats;
	}
	void I_NPCState.OnExit(Transform npc)
	{
		npc.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	I_NPCState I_NPCState.Update(Transform npc, float dt)
	{
		Vector2 dir = player.position - npc.position;
        Vector2 vel = dir.normalized * stats.Speed;

        npc.GetComponent<Rigidbody2D>().velocity = vel;

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
            c.gameObject.GetComponent<PlayerController>().Hit(stats.Damage, npc);
        }
		return null;
	}
}
