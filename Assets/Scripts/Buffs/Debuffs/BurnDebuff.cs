// Probably shouldn't cause flinch

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BurnHeroDebuff : Buff
{
    // Hero stats
    private HeroStats stats;
    // Hero controller
    private HeroController cont;

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
    public BurnHeroDebuff(int duration)
    {
        timer = 0;
        seconds = duration;
    }

    public override void OnBegin(Transform character)
    {
        chr = character;
        stats = character.GetComponent<HeroStats>();
        cont = character.GetComponent<HeroController>();
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
}

public class BurnMobDebuff : Buff
{
    // Hero stats
    private MobStats stats;
    // Hero controller
    private MobController cont;

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
    public BurnMobDebuff(int duration)
    {
        timer = 0;
        seconds = duration;
    }

    public override void OnBegin(Transform character)
    {
        chr = character;
        stats = chr.GetComponent<MobStats>();
        cont = chr.GetComponent<MobController>();
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
            cont.HitNoFlinch(dam, chr.position);

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
}
