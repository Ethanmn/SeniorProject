using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //private HeroStats stats; CHARACTER STATS

    // State the character starts in
    protected I_CharacterState startState;

    // The current state
    private I_CharacterState state;
    public I_CharacterState State
    {
        get { return state; }
    }

    public virtual void Start()
    {
        state = startState;
        state.OnEnter(transform);

        // Characters should set up stats here
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Update the state
        I_CharacterState newState = state.HandleInput(transform);
        if (newState != null)
        {
            SwitchState(newState);
        }

        newState = state.Update(transform, Time.deltaTime);

        if (newState != null)
        {
            SwitchState(newState);
        }
    }
    // Called once per 2 frames (on physics update)
    public virtual void FixedUpdate()
    {
        // Check for death
        /* if (stats.Dead && !state.Equals(deathState))
         {
             Debug.Log(gameObject.name + " down!");
             SwitchState(deathState);
         }*/

        // Update on fixed update (Probably not necessary)
        /*
        I_CharacterState newState = state.HandleInput(transform);
        if (newState != null)
        {
            SwitchState(newState);
        }
        

        newState = state.FixedUpdate(transform, Time.deltaTime);

        if (newState != null)
        {
            SwitchState(newState);
        }*/
    }

    // Switch to a new state
    protected virtual void SwitchState(I_CharacterState newState)
    {
        state.OnExit(transform);
        state = newState;
        state.OnEnter(transform);
    }

    /// <summary>
    /// Set this character's state
    /// </summary>
    /// <param name="newState"></param>
    public virtual void SetState(I_CharacterState newState)
    {
        SwitchState(newState);
    }

    /// <summary>
    /// Hit the character (with flinch) standard damage, etc.
    /// </summary>
    /// <param name="damage">Amount of damage done</param>
    /// <param name="attacker">Transform of the attacking character</param>
    /// <param name="velocity">Velocity at which this character is knocked</param>
    public virtual void Hit(int damage, Transform attacker, Vector2 velocity)
    {
        // Apply the damage
        HitDamage(damage);
    }

    /// <summary>
    /// Hit the character (NO flinch) for things like DoTs
    /// </summary>
    /// <param name="damage">Amount of damage done</param>
    /// <param name="attacker">Transform of the attacking character</param>
    public virtual void HitNoFlinch(int damage, Transform attacker)
    {
        // Apply the damage
        HitDamage(damage);
    }

    /// <summary>
    /// Apply damage (used in Hit functions)
    /// </summary>
    /// <param name="damage">Amount of damage done</param>
    protected virtual void HitDamage(int damage)
    {

    }

    // Heal the character
    /// <summary>
    /// Heal this character
    /// </summary>
    /// <param name="heal">Amount to heal this character</param>
    public virtual void Heal(int heal)
    {

    }

}