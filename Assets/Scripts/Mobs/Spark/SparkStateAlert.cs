using UnityEngine;
using System.Collections;
using System;

public class SparkStateAlert : I_MobState {

	// The player the mob has been alerted to
	private Transform hero;
	// The mob's status script
	private SparkStats stats;

    // RigidBody
    private Rigidbody2D rb;

    private float shockingTime = 2.0f;
    private float shockingTimer = 0;

    void I_ActorState.OnEnter(Transform mob)
	{
		mob.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("Sprites/Mobs/SparkPH")[1];

		hero = GameObject.FindGameObjectWithTag("Hero").gameObject.GetComponent<Transform>();

        stats = mob.GetComponent<SparkStats>();
        rb = mob.GetComponent<Rigidbody2D>();
    }
	void I_ActorState.OnExit(Transform mob)
	{
		mob.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}
	
	// Update is called once per frame
	I_ActorState I_ActorState.Update(Transform mob, float dt)
	{
        // If there is no hero to chase, go idle
        if (hero == null)
        {
            return new SparkStateIdle();
        }

        // IF Spark is in skitter
        if (stats.skitter)
        {
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

            // IF the timer has run out OR the hero is in range
            float dist = Vector2.Distance(mob.transform.position, hero.transform.position);
            if (stats.shiftTimer > stats.shiftTime ||
                (dist > 1.25f && dist < 2f))
            {
                // Turn skitter off
                Debug.Log("Skitter off");
                stats.skitter = false;
                stats.shiftTimer = 0;
            }
            else
            {
                // IF the player is too close
                if (dist <= 1.25f)
                {
                    // Move away from the player
                    Vector2 dir = mob.position - hero.position;
                    Vector2 vel = dir.normalized * stats.Speed;
                    rb.velocity = vel;
                }
                // IF the player is too far
                else if (dist > 2f)
                {
                    // Move towards the hero
                    Vector2 dir = hero.position - mob.position;
                    Vector2 vel = dir.normalized * stats.Speed;
                    rb.velocity = vel;
                }

                // Tick timer
                stats.shiftTimer += dt;
            }
        }
        // ELSE (if they are in "shock"/false)
        else
        {
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            // Shock (3 times)
            // IF an zap is active right now
            if (mob.transform.FindChild("Zap"))
            {
                // Wait 2 seconds
                shockingTimer = 0;
            }
            // IF NO zap is active
            else
            {
                // IF Zaps are on CD
                if (stats.zapCooldownTimer < stats.zapCooldown)
                {
                    stats.zapCooldownTimer += dt;
                }
                else
                {
                    stats.zapCooldownTimer = 0;
                    // IF we are out of zaps, go back to skittering
                    if (stats.shockCounter >= stats.shockCount)
                    {
                        // Turn skitter on
                        stats.skitter = true;
                        stats.shockCounter = 0;
                    }
                    // IF there are still more Zaps to be had and they're not on cooldown
                    else
                    {
                        // Spawn shock
                        Debug.Log("ZZzzzAP!");
                        GameObject attack = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/SparkAttack"));
                        attack.transform.SetParent(mob, false);
                        attack.transform.localPosition = Vector2.zero;
                        attack.name = "Zap";

                        // Increase counter
                        stats.shockCounter++;


                        shockingTimer += dt;
                    }
                }
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
            c.gameObject.GetComponent<HeroController>().Hit(stats.Damage, mob, Vector2.zero);
        }
		return null;
	}

    public I_ActorState OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }

}
