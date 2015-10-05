using System;
using UnityEngine;

class YellowPotionBuff : Buff
{
    // Duration of the buff
    private float timer;
    private float time = 10f;
    // Bonus damage given by buff
    private int dmg = 3;

    // Stats of the hero
    HeroStats stats;

    public override void OnBegin(Transform character)
    {
        // Get the stats
        stats = character.GetComponent<HeroStats>();
        // Add bonus speed
        if (stats.Apothecary)
        {
            stats.BonusDamage += dmg * 2;
        }
        else
        {
            stats.BonusSpeed += dmg;
        }
        // Reset the timer
        timer = time;
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

    public override void Refresh()
    {
        timer = time;
    }
}

