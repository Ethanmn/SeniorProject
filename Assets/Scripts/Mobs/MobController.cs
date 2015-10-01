using UnityEngine;
using System.Collections;

public class MobController : ActorController{
    /* 
        Inherited values
        I_CharacterState startState
    */
	protected I_MobFlinchState flinchState;
    protected I_MobState deathState;

	protected MobStats stats;

	// Use this for initialization
	public override void Start () {
        base.Start();

		stats = gameObject.GetComponent<MobStats>();
	}

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (stats.Dead && !state.Equals(deathState))
        {
            Debug.Log(gameObject.name + " down!");
            SwitchState(deathState);
        }
    }

    void OnCollisionStay2D(Collision2D c)
	{
        if (c.gameObject.CompareTag("Hero") || c.gameObject.CompareTag("Mob"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

		I_ActorState newState = state.OnCollisionStay(transform, c);
		if(newState != null)
		{
			SwitchState(newState);
		}
	}

	public override void Hit(int damage, Transform attacker, Vector2 velocity)
	{
        base.Hit(damage, attacker, velocity);

        // Make the mob flinch
		if (!state.Equals(flinchState.GetType()))
		{
			flinchState.SetVel(velocity);
			SetState(flinchState);

            if (stats.Dead)
            {
                PublisherBox.onKillPub.RaiseEvent(transform);
            }
		}
	}

    public override void HitNoFlinch(int damage, Transform attacker)
    {
        base.HitNoFlinch(damage, attacker);

        if (stats.Dead)
        {
            PublisherBox.onKillPub.RaiseEvent(transform);
        }
    }

    protected override void HitDamage(int damage)
    {
        base.HitDamage(damage);

        if (!state.GetType().Equals(flinchState.GetType()))
        {
            // Deal the damage
            stats.Health -= damage;
        }
    }

    public override void Heal(int heal)
    {
        base.Heal(heal);

        // Heal the amount
        stats.Health += heal;
    }
}
