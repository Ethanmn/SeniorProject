using UnityEngine;

class DarkMarkBuff : TrinketBuff
{
    // The amount of bonus damage DM gives
    private int bonusDamage = 5;
    // The amount of health DM takes
    private int bonusHealth = -3;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase damage
        stats.BonusDamage += bonusDamage;
        // Decrease health
        stats.BonusMaxHealth += bonusHealth;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Increase damage
        stats.BonusDamage -= bonusDamage;
        // Decrease health
        stats.BonusMaxHealth -= bonusHealth;
    }
}
