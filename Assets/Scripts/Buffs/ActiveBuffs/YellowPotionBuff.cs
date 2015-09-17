using System;
using UnityEngine;

class YellowPotionBuff : Buff
{
    // Duration of the buff
    private float timer;
    // Bonus damage given by buff
    private int dmg = 3;

    // Stats of the hero
    HeroStats stats;

    public override void OnBegin(Transform character)
    {
        // Get the stats
        stats = character.GetComponent<HeroStats>();
        // Add bonus speed
        stats.BonusDamage += dmg;
        // Reset the timer
        timer = 10f;
    }

    public override void OnEnd()
    {
        // Remove the bonus speed
        stats.BonusDamage -= dmg;
    }

    public override void OnUpdate()
    {
        if (timer <= 0)
        {
            remove = true;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}

