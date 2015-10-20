using System;
using UnityEngine;

class OrangePotionBuff : Buff
{
    // Duration of the buff
    private float timer;
    private float time = 10f;
    // Bonus damage given by buff
    private int dmg = 2;
    // Bonus attack speed
    private float atkSpd = 0.33f;
    // Bonus max ammo
    private int ammo = 4;

    // Multiplier for Apothecary buff
    private int mult = 1;

    // Stats of the hero
    HeroStats stats;

    public override void OnBegin(Transform character)
    {
        // Get the stats
        stats = character.GetComponent<HeroStats>();

        // Add the bonuses
        if (stats.Apothecary)
        {
            mult = 2;
        }
        else
        {
            mult = 1;
        }
        // Add bonus damage
        stats.BonusDamage += dmg * mult;
        // Add bonus attack speed
        stats.BonusSwingTimeMultiplier -= atkSpd * mult;
        // Add bonus max ammo
        stats.BonusMaxAmmo += ammo * mult;
        // Reset the timer
        timer = time;
    }

    public override void OnEnd()
    {
        // Remove the bonus damage
        stats.BonusDamage -= dmg * mult;
        // Remove bonus attack speed
        stats.BonusSwingTimeMultiplier += atkSpd * mult;
        // Remove bonus max ammo
        stats.BonusMaxAmmo -= ammo * mult;
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

