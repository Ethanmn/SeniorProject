using UnityEngine;
using System.Collections;

public class MobController : MonoBehaviour{
	protected I_MobState startState;
	protected I_MobFlinchState flinchState;
    protected I_MobState deathState;

	protected I_MobState state;
	protected MobStats stats;

	// Use this for initialization
	public virtual void Start () {
		stats = gameObject.GetComponent<MobStats>();
		
		state = startState;
		state.OnEnter(transform, stats);
	}

	// Update is called once per frame
	void Update ()
	{
		I_MobState newState = state.HandleInput(transform);
		if(newState != null)
		{
			SwitchState(newState);
		}
		
		newState = state.Update(transform, Time.deltaTime);
		
		if(newState != null)
		{
			SwitchState(newState);
		}
	}

	void FixedUpdate ()
	{
		if (stats.Dead && !state.Equals(deathState))
		{
			Debug.Log(gameObject.name + " down!");
            SwitchState(deathState);
		}

        I_MobState newState = state.HandleInput(transform);
        if (newState != null)
        {
            SwitchState(newState);
        }

        newState = state.FixedUpdate(transform, Time.deltaTime);

        if (newState != null)
        {
            SwitchState(newState);
        }
    }
	
	void OnCollisionStay2D(Collision2D c)
	{
        if (c.gameObject.CompareTag("Hero") || c.gameObject.CompareTag("Mob"))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

		I_MobState newState = state.OnCollisionStay(transform, c);
		if(newState != null)
		{
			SwitchState(newState);
		}
	}

	// Switch to a new state
	private void SwitchState(I_MobState newState)
	{
		state.OnExit(transform);
		state = newState;
		state.OnEnter(transform, stats);
	}

    // Return the current mob state
    public I_MobState getState()
    {
        return this.state;
    }

    // Set the current mob state
    public void SetState(I_MobState newState)
	{
		SwitchState(newState);
	}

	public void Hit(int damage, Vector2 vel)
	{
		if (!state.Equals(flinchState.GetType()))
		{
			flinchState.SetVel(vel);
			SetState(flinchState);
			
			// Take the damage
			stats.Health -= damage;
		}
	}
}
