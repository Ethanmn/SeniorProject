using UnityEngine;

class MischiviousBuff : AttributeBuff
{
    // Amount of max move speed to work
    private float bonusFlinchtime = 1.0f;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase max speed
        stats.BonusFlinchTime += bonusFlinchtime;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Decrease max speed
        stats.BonusSpeed -= bonusFlinchtime;
    }
}
