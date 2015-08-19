using UnityEngine;

class WorkhorseBuff : AttributeBuff
{
    // The amount of health DM takes
    private int bonusHealth = 2;

    public override void OnBegin(Transform character)
    {
        base.OnBegin(character);

        // Simply add stats
        // Increase health
        stats.BonusMaxHealth += bonusHealth;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // Remove given stats
        // Decrease health
        stats.BonusMaxHealth -= bonusHealth;
    }
}
