using UnityEngine;
using System.Collections.Generic;

public class MastermindStateIdle : I_MobState {

	MastermindStats stats;

	void I_ActorState.OnEnter(Transform mob)
	{
		//mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/Mobs/Mastermind")[0];
		mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		stats = mob.GetComponent<MastermindStats>();
        if (stats == null)
        {
            Debug.Log("ERROR NO STATS");
        }
	}
	void I_ActorState.OnExit(Transform mob)
	{

	}
	
	// Update is called once per frame
	I_ActorState I_ActorState.Update(Transform mob, float dt)
	{
		GameObject hero = GameObject.FindGameObjectWithTag("Hero");

		mob.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Find all the mobs in the room
        GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
        foreach (GameObject mb in mobs)
        {
            // If they aren't on the list and it's not yourself
            if (!stats.Tethers.ContainsKey(mb) && !(mb.Equals(mob.gameObject)))
            {
                // Create a new tether
                GameObject newTether = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Tether"));
                newTether.transform.SetParent(mob, false);
                newTether.GetComponent<TetherStats>().Master = mob.gameObject;
                newTether.GetComponent<TetherStats>().Target = mb.gameObject;
                int i = Random.Range(0, stats.BuffsList.Length);
                newTether.GetComponent<TetherStats>().TetherBuff = stats.BuffsList[i];

                // Add them to the dict and give them a tether
                stats.Tethers.Add(mb, newTether);
            }
        }

        return null;
	}
    I_MobState I_MobState.FixedUpdate(Transform mob, float dt)
    {
        return null;
    }
    I_ActorState I_ActorState.HandleInput(Transform mob)
	{
		return null;
	}

	I_ActorState I_ActorState.OnCollisionStay(Transform mob, Collision2D c)
	{
        if (c.gameObject.CompareTag("Hero"))
		{
			c.gameObject.GetComponent<HeroController>().Hit(stats.damage, mob, Vector2.zero);
		}

		return null;
	}

    public I_ActorState OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }

    private Buff RandBuff()
    {
        return null;
    }
}
