using UnityEngine;
using System.Collections;

public class DestructableController : ActorController
{
    // Stats of the destructable object
    DestructableStats stats;

    public override void Start()
    {
        // Starting state of the destructable
        startState = new DestructableStateWhole();

        // Set stats
        stats = GetComponent<DestructableStats>();

        base.Start();
    }


    /// <summary>
    /// Hit the destructable
    /// </summary>
    /// <param name="damage">How much damage is dealt to the destructable</param>
    /// <param name="attacker">Who attacked it</param>
    /// <param name="velocity">Unused</param>
    public override void Hit(int damage, Transform attacker, Vector2 velocity)
    {
        if (State.GetType().Equals(typeof(DestructableStateWhole)))
        {
            // Deal damage
            stats.CurHealth -= damage;

            // IF it is out of health, it breaks
            if (stats.CurHealth <= 0)
            {
                // Break it
                SetState(new DestructableStateBroken());
            }
        }
    }
}
