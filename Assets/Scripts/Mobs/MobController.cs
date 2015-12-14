using UnityEngine;
using UnityEngine.UI;

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
		}
	}

    public override void HitNoFlinch(int damage, Transform attacker)
    {
        base.HitNoFlinch(damage, attacker);
    }

    protected override void HitDamage(int damage)
    {
        base.HitDamage(damage);

        if (!state.GetType().Equals(flinchState.GetType()))
        {
            // Deal the damage
            ChangeHealth(damage * -1);
        }
    }

    public override void Heal(int heal)
    {
        base.Heal(heal);

        // Heal the amount
        ChangeHealth(heal);
    }

    protected override void ChangeHealth(int change)
    {
        int healthChange;
        int oldHealth = stats.Health;

        stats.Health += change;
        healthChange = stats.Health - oldHealth;

        Canvas can = gameObject.GetComponentInChildren<Canvas>();
        GameObject text = Instantiate(Resources.Load("Prefabs/UIHealthChangeText")) as GameObject;
        text.GetComponent<Text>().text = (healthChange >= 0 ? "+" : "") + healthChange.ToString();
        text.transform.SetParent(can.transform, false);
    }
}
