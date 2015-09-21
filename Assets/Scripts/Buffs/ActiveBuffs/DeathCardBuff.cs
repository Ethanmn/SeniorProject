using System;
using UnityEngine;

class DeathCardBuff : Buff
{
    // Duration of the buff
    private float timer;
    // Bonus damage given by buff
    private int dmg = 10;
    // Bonus attack speed
    private float atkSpd = 0.9f;
    // Bonus max ammo
    private int ammo = 8;

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
        stats.BonusMaxAmmo += ammo;
        // Reset the timer
        timer = 5f;
    }

    public override void OnEnd()
    {
        // Remove the bonus damage
        stats.BonusDamage -= dmg;
        // Remove bonus attack speed
        stats.BonusSwingTimeMultiplier += atkSpd;
        // Remove bonus max ammo
        stats.BonusMaxAmmo -= ammo;
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

