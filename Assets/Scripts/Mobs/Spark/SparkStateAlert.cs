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

        // Randomize the shift time
        stats.shiftTime = stats.baseShiftTime + UnityEngine.Random.Range(-1.0f, 1.0f);
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

            // IF the timer has run out
            float dist = Vector2.Distance(mob.transform.position, hero.transform.position);
            if (stats.shiftTimer > stats.shiftTime)
            {
                // Turn skitter off
                Debug.Log("Skitter off");
                stats.skitter = false;
                stats.shiftTimer = 0;
            }
            else
            {
                int changeDir = UnityEngine.Random.Range(0, 101);
                Vector2 dir = rb.velocity.normalized;
                Vector2 vel = rb.velocity;

                // Randomly change velocity
                if (changeDir <= 3 /* || it hit a wall*/)
                {
                    dir = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
                    vel = dir.normalized * stats.Speed;
                }
                
                rb.velocity = vel;

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
                        // Randomize the shift time
                        stats.shiftTime = stats.baseShiftTime + UnityEngine.Random.Range(-1.0f, 1.0f);

                        // Turn skitter on
                        stats.skitter = true;
                        stats.shockCounter = 0;
                    }
                    // IF there are still more Zaps to be had and they're not on cooldown
                    else
                    {
                        // Spawn shock
                        GameObject attack = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/SparkAttack"));
                        attack.transform.SetParent(mob, false);
                        attack.transform.localPosition = Vector2.zero;
                        attack.name = "Zap";
                        attack.GetComponent<SparkAttack>().Spark = mob;
                        attack.GetComponent<AttackStats>().Damage = stats.Damage;

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

        // IF it hits a wall, change directions that isn't towards the wall
        if (c.gameObject.CompareTag("Wall"))
        {
            // Get a direction that is away from the wall
            Vector2 dir = -1 *(mob.position - c.transform.position);
            Vector2 vel = dir.normalized * stats.Speed;

            rb.velocity = vel;
        }
        return null;
	}

    public I_ActorState OnCollisionEnter(Transform actor, Collision2D c)
    {
        return null;
    }

}
