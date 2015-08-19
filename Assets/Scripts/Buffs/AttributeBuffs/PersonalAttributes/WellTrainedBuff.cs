using UnityEngine;

class WellTrainedBuff : AttributeBuff
{
    // The amount of damage to add
    private int bonusDamage = 1;
    // Amount of max move speed to work
    private float bonusMovespeed = 2.0f;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase damage
        stats.BonusDamage += bonusDamage;
        // Increase max speed
        stats.BonusSpeed += bonusMovespeed;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Decrease damage
        stats.BonusDamage -= bonusDamage;
        // Decrease max speed
        stats.BonusSpeed -= bonusMovespeed;
    }
}
