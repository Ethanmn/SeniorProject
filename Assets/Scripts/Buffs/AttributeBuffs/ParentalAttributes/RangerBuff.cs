// Stat doesn't exist yet!

using UnityEngine;

class RangerBuff : AttributeBuff
{
    // Amount of max ammo to add
    private float attackSpeed = 0.3f;
    // Reload multiplier (double)
    private float reloadSpeed = 0.15f;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase attack speed
        stats.BonusSwingTimeMultiplier -= attackSpeed;
        // Increase reload speed
        stats.BonusReloadTime += reloadSpeed;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Decrease attack speed
        stats.BonusSwingTimeMultiplier += attackSpeed;
        // Decrease reload speed
        stats.BonusReloadTime -= reloadSpeed;
    }
}
