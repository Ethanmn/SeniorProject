using UnityEngine;

class NimbleBuff : AttributeBuff
{
    // Amount of max move speed to work
    private float attackSpeedMult = -0.5f;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase attack speed
        stats.BonusSwingTimeMultiplier += attackSpeedMult;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Decrease attack speed
        stats.BonusSwingTimeMultiplier -= attackSpeedMult;
    }
}
