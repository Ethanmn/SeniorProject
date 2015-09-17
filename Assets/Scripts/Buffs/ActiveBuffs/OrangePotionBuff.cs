using System;
using UnityEngine;

class OrangePotionBuff : Buff
{
    // Duration of the buff
    private float timer;
    // Bonus damage given by buff
    private int dmg = 2;
    // Bonus attack speed
    private float atkSpd = 0.33f;
    // Bonus max ammo
    private int ammo = 4;

    // Stats of the hero
    HeroStats stats;

    public override void OnBegin(Transform character)
    {
        // Get the stats
        stats = character.GetComponent<HeroStats>();
        // Add bonus damage
        stats.BonusDamage += dmg;
        // Add bonus attack speed
        stats.BonusSwingTimeMultiplier -= atkSpd;
        // Add bonus max ammo
        stats.BonusMaxAmmo += 4;
        // Reset the timer
        timer = 10f;
    }

    public override void OnEnd()
    {
        // Remove the bonus damage
        stats.BonusDamage -= dmg;
        // Remove bonus attack speed
        stats.BonusSwingTimeMultiplier += atkSpd;
        // Remove bonus max ammo
        stats.BonusMaxAmmo -= 4;
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

