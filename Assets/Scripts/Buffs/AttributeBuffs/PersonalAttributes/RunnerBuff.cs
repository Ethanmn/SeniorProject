using UnityEngine;

class RunnerBuff : AttributeBuff
{
    // Amount of max move speed to work
    private float bonusMovespeed = 2.0f;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase max speed
        stats.BonusSpeed += bonusMovespeed;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Decrease max speed
        stats.BonusSpeed -= bonusMovespeed;
    }
}
