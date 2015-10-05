// Probably shouldn't cause flinch

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BurnDebuff : Buff
{
    // Hero controller
    private ActorController cont;

    // Timer for debuff duration
    private float timer;
    // Seconds tracker
    private int seconds;

    // Base damage dealt per tick
    private int dam = 1;

    /// <summary>
    /// Create a new burn debuff
    /// </summary>
    /// <param name="duration">Duration of the burn.</param>
    public BurnDebuff(int duration)
    {
        timer = 0;
        seconds = duration;
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
            cont.HitNoFlinch(dam, chr);

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
    }
}