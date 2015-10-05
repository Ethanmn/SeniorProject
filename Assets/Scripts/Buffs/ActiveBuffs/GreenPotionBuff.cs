using System;
using UnityEngine;

class GreenPotionBuff : Buff
{
    // Duration of the buff
    private float timer;
    private float time = 10f;
    // Bonus damage given by buff
    private int spd = 3;

    // Stats of the hero
    HeroStats stats;

    public override void OnBegin(Transform character)
    {
        // Get the stats
        stats = character.GetComponent<HeroStats>();

        // Add bonus speed
        if (stats.Apothecary)
        {
            stats.BonusSpeed += spd * 2;
        }
        else
        {
            stats.BonusSpeed += spd;
        }
        
        // Reset the timer
        timer = time;
    }

    public override void OnEnd()
    {
        // Remove the bonus speed
        stats.BonusSpeed -= spd;
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

