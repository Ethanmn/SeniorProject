using UnityEngine;

class FarmerBuff : AttributeBuff
{
    // Amount of max move speed to work
    private float healthMult = 1.0f;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase heal multiplier
        stats.HealMultiplier += healthMult;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Decrease heal multiplier
        stats.HealMultiplier -= healthMult; 
    }
}
