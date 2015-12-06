using UnityEngine;

public class BurnDebuff : Buff
{
    // Hero controller
    private ActorController cont;

    // Timer for debuff duration
    private float timer;
    // Seconds tracker
    private int seconds;

    // Amount of damage to do in total
    private int damagePool = 1;

    /// <summary>
    /// Create a new burn debuff
    /// </summary>
    /// <param name="damage">Totale damage over 2 seconds of the burn.</param>
    public BurnDebuff(int damage)
    {
        timer = 0;
        seconds = 2;
        //damagePool = damage;
    }

    public override void OnBegin(Transform character)
    {
        chr = character;
        cont = character.GetComponent<ActorController>();
    }

    public override void OnEnd()
    {
        
    }

    public override void OnUpdate()
    {
        // IF the burn has not run out
        if (timer < 1)
        {
            // Count up
            timer += Time.deltaTime;
        }
        else
        {
            // Deal damage
            cont.HitNoFlinch(damagePool, chr);

            seconds--;

            // Reset the timer
            timer = 0;
        }

        // If the ticks have run out
        if (seconds <= 0)
        {
            // Remove the buff
            remove = true;
        }
    }

    public override void Refresh()
    {
        // Add two more ticks
        seconds += 2;
        // Add damage to the damage pool

    }
}